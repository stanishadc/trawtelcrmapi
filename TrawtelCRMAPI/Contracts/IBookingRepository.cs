using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBookingRepository : IRepositoryBase<Booking>
    {
        IEnumerable<Booking> GetAllBookings();
        Booking GetBookingById(Guid BookingId);
        List<Booking> GetBookingByClientId(Guid ClientId);
        List<Booking> GetBookingByTravelType(char TravelType);
        void CreateBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void DeleteBooking(Booking booking);
    }
}
