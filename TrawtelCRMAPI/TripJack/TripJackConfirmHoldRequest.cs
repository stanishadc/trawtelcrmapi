using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripJack
{
    public class TripJackConfirmHoldRequest
    {
        public string bookingId { get; set; }
        public List<PaymentInfo> paymentInfos { get; set; }
    }
}
