using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripJack
{
    public class TripJackBookingDetailsResponse
    {
        public Order order { get; set; }
        public ItemInfos itemInfos { get; set; }
        public GstInfo gstInfo { get; set; }
        public Status status { get; set; }
    }
    public class Order
    {
        public string bookingId { get; set; }
        public double amount { get; set; }
        public double markup { get; set; }
        public string orderType { get; set; }
        public DeliveryInfo deliveryInfo { get; set; }
        public string status { get; set; }
        public DateTime createdOn { get; set; }
    }
    public class NCM
    {
        public double TDS { get; set; }
        public int OC { get; set; }
    }
    public class AIR
    {
        public List<TripInfo> tripInfos { get; set; }
        public List<TravellerInfo> travellerInfos { get; set; }
        public TotalPriceInfo totalPriceInfo { get; set; }
    }
    public class ItemInfos
    {
        public AIR AIR { get; set; }
    }
}
