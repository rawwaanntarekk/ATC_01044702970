using Areeb.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Event_Booking_System.Controllers
{
    public class EventController(IEventRepository _eventRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var events = await _eventRepository.GetAllEventsAsync();
            return View(events);
        }
    }
}
