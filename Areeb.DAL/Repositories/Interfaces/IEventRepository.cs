using Areeb.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.DAL.Repositories.Interfaces
{
    internal interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(int id);
        Task AddEventAsync(Event applicationEvent);
        void UpdateEventAsync(Event applicationEvent);
        void DeleteEventAsync(int id);
    }
}
