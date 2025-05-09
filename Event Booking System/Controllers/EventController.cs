using Areeb.DAL.Entities;
using Areeb.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Event_Booking_System.Controllers
{
    public class EventController(IGenericRepository<Event> _eventRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var events = await _eventRepository.GetAllAsync();
            return View(events);
        }

        public async Task<IActionResult> Details(int id)
        {
            var eventDetails = await _eventRepository.GetByIdAsync(id);
            if (eventDetails == null)
                return NotFound();

            return View(eventDetails);
        }
    }
}
