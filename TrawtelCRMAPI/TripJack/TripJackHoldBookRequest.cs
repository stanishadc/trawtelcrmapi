using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripJack
{
    public class TripJackHoldBookRequest
    {
        public string bookingId { get; set; }
        public List<TravellerInfo> travellerInfo { get; set; }
        public DeliveryInfo deliveryInfo { get; set; }
    }
}
