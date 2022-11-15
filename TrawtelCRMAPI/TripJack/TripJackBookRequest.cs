using System.Collections.Generic;

namespace TripJack
{
    public class TripJackBookRequest
    {
        public string bookingId { get; set; }
        public List<PaymentInfo> paymentInfos { get; set; }
        public List<TravellerInfo> travellerInfo { get; set; }
        public GstInfo gstInfo { get; set; }
        public DeliveryInfo deliveryInfo { get; set; }
    }
    public class PaymentInfo
    {
        public double amount { get; set; }
    }

    public class TravellerInfo
    {
        public string ti { get; set; }
        public string fN { get; set; }
        public string lN { get; set; }
        public string pt { get; set; }
        public string dob { get; set; }
    }

    public class GstInfo
    {
        public string gstNumber { get; set; }
        public string email { get; set; }
        public string registeredName { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
    }

    public class DeliveryInfo
    {
        public List<string> emails { get; set; }
        public List<string> contacts { get; set; }
    }
}
