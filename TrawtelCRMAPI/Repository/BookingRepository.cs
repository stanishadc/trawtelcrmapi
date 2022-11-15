using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
    {
        public BookingRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<Booking> GetAllBookings()
        {
            return FindAll()
                .OrderBy(ow => ow.CreatedDate)
                .ToList();
        }
        public Booking GetBookingById(Guid BookingId)
        {
            return FindByCondition(client => client.BookingId.Equals(BookingId)).FirstOrDefault();
        }
        public List<Booking> GetBookingByClientId(Guid ClientId)
        {
            return FindByCondition(client => client.ClientId.Equals(ClientId)).ToList();
        }
        public List<Booking> GetBookingByTravelType(char TravelType)
        {
            return FindByCondition(client => client.TravelType.Equals(TravelType)).ToList();
        }
        public void CreateBooking(Booking client)
        {
            Create(client);
        }
        public void UpdateBooking(Booking client)
        {
            Update(client);
        }
        public void DeleteBooking(Booking client)
        {
            Delete(client);
        }
    }
}
