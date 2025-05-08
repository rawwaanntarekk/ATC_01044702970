using Areeb.DAL;
using Areeb.DAL.Entities;
using Areeb.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.BLL
{
    public class BookingService : IBookingService
    {
        private readonly IGenericRepository<Booking> _bookingRepository;
        private readonly IGenericRepository<Event> _eventRepository;

        public BookingService(IGenericRepository<Booking> BookingRepository, IGenericRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
            _bookingRepository = BookingRepository;
        }
        public async Task Book(int eventId, string username, int quantity)
        {
            // 1. Checking availability of tickets
            var eventToBook = await _eventRepository.GetByIdAsync(eventId);
            if (eventToBook.TicketsAvailable <= 0)
                throw new Exception("No tickets available for this event.");

            // 2. Creating a new booking
            var booking = new Booking
            {
                EventId = eventId,
                CustomerName = username,
                Quantity = quantity,
                BookingDate = DateTime.Now,
                IsPaid = true
            };

            //3. Adding the booking to the database
            await _bookingRepository.AddAsync(booking);







        }

        public Task CancelBooking(int bookingId)
        {
            throw new NotImplementedException();
        }
    }
}
