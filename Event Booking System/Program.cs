using Areeb.BLL;
using Areeb.BLL.Services.Mail;
using Areeb.DAL;
using Areeb.DAL.Data.Seeds;
using Areeb.DAL.Entities;
using Areeb.DAL.Repositories.Implementations;
using Areeb.DAL.Repositories.Interfaces;
using Event_Booking_System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Event_Booking_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<DataSeeding>();
            builder.Services.AddScoped<EventRepository>();
            builder.Services.AddScoped<IGenericRepository<Event>, EventRepository>();
            builder.Services.AddScoped<IGenericRepository<Booking>, BookingRepository>();
            builder.Services.AddScoped<IBookingService , BookingService>();
            builder.Services.AddScoped<BookingMailService>();
            builder.Services.AddScoped<EmailTemplateService>();
            
            // Register Configuration service
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Injecting the DataSeeding class to seed the database
            var scope = app.Services.CreateScope();
            var DataSeedObject = scope.ServiceProvider.GetRequiredService<DataSeeding>();
            DataSeedObject.SeedData();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/" && !context.User.Identity!.IsAuthenticated)
                {
                    context.Response.Redirect("/Identity/Account/Login");
                    return;
                }
                else if (context.Request.Path == "/" && context.User.Identity!.IsAuthenticated)
                {
                    context.Response.Redirect("/Home/Index");
                    return;
                }

                await next();
            });



            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
