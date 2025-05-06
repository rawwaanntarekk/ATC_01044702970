using Areeb.DAL.Entities;
using Areeb.DAL.Repositories.Interfaces;
using Event_Booking_System.Data;
using Microsoft.EntityFrameworkCore;

namespace Areeb.DAL.Repositories.Implementations
{
    public class EventRepository(ApplicationDbContext applicationDbContext) : IEventRepository
    {
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task<IEnumerable<Event>> GetAllEventsAsync() => await _applicationDbContext.Events.ToListAsync();

        public async Task<Event> GetEventByIdAsync(int id)
        {
            var eventEntity = await _applicationDbContext.Events.FirstOrDefaultAsync(e => e.Id == id);
            return eventEntity ?? throw new Exception($"Event with ID {id} not found.");
        }

        public async Task AddEventAsync(Event applicationEvent)
        {
              await _applicationDbContext.Events.AddAsync(applicationEvent);
              await _applicationDbContext.SaveChangesAsync();
        }
        public  void UpdateEventAsync(Event applicationEvent)
        {
            var existingEvent = _applicationDbContext.Events.FirstOrDefault(e => e.Id == applicationEvent.Id) ?? throw new Exception($"Event with ID {applicationEvent.Id} not found.");
            _applicationDbContext.Update(applicationEvent);
        }

        public void DeleteEventAsync(int id)
        {
            var eventEntity = _applicationDbContext.Events.FirstOrDefault(e => e.Id == id) ?? throw new Exception($"Event with ID {id} not found.");
            _applicationDbContext.Events.Remove(eventEntity);
        }

       

        

    }
}
