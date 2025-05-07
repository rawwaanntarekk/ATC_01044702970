using Areeb.DAL.Entities;
using Event_Booking_System.Data;
using Microsoft.EntityFrameworkCore;

namespace Areeb.DAL.Data.Seeds
{
    public class DataSeeding(ApplicationDbContext _applicationDbContext)
    {

        public void SeedData()
        {
            // Checking if there are any pending migrations, migrating if any are found
            if (_applicationDbContext.Database.GetPendingMigrations().Any())
            {
                _applicationDbContext.Database.Migrate();
            }

            _applicationDbContext.Events.RemoveRange(_applicationDbContext.Events);
            _applicationDbContext.Bookings.RemoveRange(_applicationDbContext.Bookings);

            _applicationDbContext.SaveChanges();

            _applicationDbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Events', RESEED, 0)");
            _applicationDbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Bookings', RESEED, 0)");

            if (!_applicationDbContext.Events.Any())
            {
                var events = new List<Event>
                {
                    new() {
                        Name = "Concert",
                        Description = "Live concert featuring popular bands.",
                        StartDate = DateTime.Now.AddDays(30),
                        EndDate = DateTime.Now.AddDays(30).AddHours(3),
                        Location = "City Stadium",
                        Price = 50.00m,
                        Capacity = 1000
                    },
                    new() {
                        Name = "Art Exhibition",
                        Description = "Showcasing local artists' work.",
                        StartDate = DateTime.Now.AddDays(60),
                        EndDate = DateTime.Now.AddDays(60).AddHours(5),
                        Location = "Art Gallery",
                        Price = 20.00m,
                        Capacity = 500
                    },
                    new() {
                        Name = "Tech Conference",
                        Description = "Annual tech conference with industry leaders.",
                        StartDate = DateTime.Now.AddDays(90),
                        EndDate = DateTime.Now.AddDays(90).AddHours(8),
                        Location = "Convention Center",
                        Price = 150.00m,
                        Capacity = 2000
                    },
                    new() {
                        Name = "Food Festival",
                        Description = "Taste dishes from around the world.",
                        StartDate = DateTime.Now.AddDays(120),
                        EndDate = DateTime.Now.AddDays(120).AddHours(6),
                        Location = "City Park",
                        Price = 10.00m,
                        Capacity = 3000
                    },
                    new() {
                        Name = "Charity Run",
                        Description = "5K run to raise funds for local charities.",
                        StartDate = DateTime.Now.AddDays(150),
                        EndDate = DateTime.Now.AddDays(150).AddHours(4),
                        Location = "Downtown",
                        Price = 25.00m,
                        Capacity = 800
                    }

                };

                _applicationDbContext.Events.AddRange(events);
                _applicationDbContext.SaveChanges();
            }



            if (!_applicationDbContext.Bookings.Any())
            {
                var bookings = new List<Booking>
                {
                    new() {
                        EventId = 1,
                        CustomerName = "John Doe",
                        Quantity = 2,
                        BookingDate = DateTime.Now,
                        IsPaid = true
                    },
                    new() {
                        EventId = 2,
                        CustomerName = "Jane Smith",
                        Quantity = 4,
                        BookingDate = DateTime.Now,
                        IsPaid = false
                    },
                    new() {
                        EventId = 3,
                        CustomerName = "Alice Johnson",
                        Quantity = 1,
                        BookingDate = DateTime.Now,
                        IsPaid = true
                    },
                    new() {
                        EventId = 4,
                        CustomerName = "Bob Brown",
                        Quantity = 3,
                        BookingDate = DateTime.Now,
                        IsPaid = false
                    },
                    new() {
                        EventId = 5,
                        CustomerName = "Charlie Davis",
                        Quantity = 5,
                        BookingDate = DateTime.Now,
                        IsPaid = true
                    }
                };
                _applicationDbContext.Bookings.AddRange(bookings);
                _applicationDbContext.SaveChanges();
            }


        }
    }
}
