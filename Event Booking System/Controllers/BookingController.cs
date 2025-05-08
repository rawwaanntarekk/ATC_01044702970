using Areeb.BLL;
using Areeb.DAL.Entities;
using Areeb.DAL.Repositories.Implementations;
using Areeb.DAL.Repositories.Interfaces;
using Event_Booking_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class BookingController : Controller
{
    private readonly IGenericRepository<Event> _eventRepository;
    private readonly IBookingService _bookingService;

    public BookingController(IGenericRepository<Event> eventRepository, IBookingService bookingService)
    {
        _eventRepository = eventRepository;
        _bookingService = bookingService;
    }

    [HttpGet]
    public async Task<IActionResult> Create(int id)
    {
        var eventEntity = await _eventRepository.GetByIdAsync(id);
        if (eventEntity == null)
        {
            return NotFound();
        }

        var model = new CreateBookingViewModel
        {
            EventId = id,
            EventName = eventEntity.Name 
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookingViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(model.EventId);
            if (eventEntity == null)
            {
                return NotFound();
            }
            return View(model);
        }

        var userName = User?.Identity?.Name;

        try
        {
            await _bookingService.Book(model.EventId, userName!, model.Quantity);
            TempData["SuccessMessage"] = "Booking created successfully!";
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            var eventEntity = await _eventRepository.GetByIdAsync(model.EventId);
            if (eventEntity == null)
            {
                return NotFound();
            }
            model.EventName = eventEntity.Name; 
            return View(model);
        }

        return RedirectToAction("Index", "Event"); 
    }
}