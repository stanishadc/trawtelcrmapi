using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripJack
{
    public class TripJackConfirmHoldResponse
    {
        public string bookingId { get; set; }
        public Status status { get; set; }
    }
}
