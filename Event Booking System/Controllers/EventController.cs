using Areeb.DAL;
using Areeb.DAL.Entities;
using Areeb.DAL.Repositories.Interfaces;
using Event_Booking_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Booking_System.Controllers
{
    public class EventController(IGenericRepository<Event> _eventRepository , IGenericRepository<Booking> _bookingRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var events = await _eventRepository.GetAllAsync();

            var userBookings = await _bookingRepository.GetAllAsync();
            userBookings = userBookings.Where(b => b.CustomerName == User.Identity!.Name).ToList();

            var eventViewModels = events.Select(e =>
            {
                var userBooking = userBookings.FirstOrDefault(b => b.EventId == e.Id );

                return new EventViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Location = e.Location,
                    IsBookedByUser = userBooking != null,
                    BookingId = (userBooking?.Id) ?? 0, 
                    Price = e.Price,
                    StartDate = e.StartDate,
                    TicketsAvailable = e.TicketsAvailable
                };
            }).ToList();

            return View(eventViewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var eventDetails = await _eventRepository.GetByIdAsync(id);
            if (eventDetails == null)
                return NotFound();

            return View(eventDetails);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventViewModel eventModel)
        {
            if (ModelState.IsValid)
            {
                Event CreatedEvent = new()
                {
                    Name = eventModel.Name,
                    Description = eventModel.Description,
                    StartDate = eventModel.StartDate,
                    EndDate = eventModel.EndDate,
                    Location = eventModel.Location,
                    Price = eventModel.Price,
                    Capacity = eventModel.Capacity,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await _eventRepository.AddAsync(CreatedEvent);
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to create event.");
            return View(eventModel);
        }
    }
}
