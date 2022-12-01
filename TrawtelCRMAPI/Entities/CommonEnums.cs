using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CommonEnums
    {
        public enum TravelType
        {
            Flight,
            Hotel,
            Visa,
            Sightseeing,
            Cars,
            CarRental,
            Packages
        }
        public enum ClassType
        {
            Economy,
            PremiumEconomy,
            Business,
            First
        }
        public enum Status
        {
            New,
            Cancelled,
            Replied,
            Responded,
            Booked,
            Expired,
            Closed
        }
        public enum UserStatus
        {
            Active,
            Pending,
            Blocked
        }
        public enum BookingStatus
        {
            Booked,
            Cancelled,
            Failed,
            Pending
        }
        public enum TravellerType
        {
            Adult,
            Kid,
            Infant
        }
        public enum VisaType
        {
            Tourist,
            Student,
            Business
        }
        public enum JourneyTypes
        {
            OneWay,
            RoundTrip,
            MultiCity
        }
        public enum UserTypes
        {
            Supplier,
            Client,
            Admin,
            Support,
            Technical,
            Agent
        }
        public enum BookingTypes
        {
            Online,
            Offline
        }
        public enum EntryTypes
        {
            Single,
            Multiple
        }
        public enum SupplierNames
        {
            TripJack,
            GRN,
            RezLive,
            TBO
        }
        public enum RefundType
        {
            NonRefundable,
            Refundable,
            PartialRefundable
        }
        public enum MealType
        {
            PaidMeal,
            FreeMeal
        }
    }
}
