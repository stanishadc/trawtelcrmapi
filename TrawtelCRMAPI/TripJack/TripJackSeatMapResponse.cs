using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripJack
{
    public class TripJackSeatMapResponse
    {
        public TripSeatMap tripSeatMap { get; set; }
        public string bookingId { get; set; }
        public Status status { get; set; }
    }
    public class SData
    {
        public int row { get; set; }
        public int column { get; set; }
    }

    public class SeatPosition
    {
        public int row { get; set; }
        public int column { get; set; }
    }

    public class SInfo
    {
        public string seatNo { get; set; }
        public SeatPosition seatPosition { get; set; }
        public bool isBooked { get; set; }
        public bool isLegroom { get; set; }
        public string code { get; set; }
        public int amount { get; set; }
        public bool? isAisle { get; set; }
    }

    public class _970
    {
        public SData sData { get; set; }
        public List<SInfo> sInfo { get; set; }
    }

    public class TripSeat
    {
        public _970 _970 { get; set; }
    }

    public class TripSeatMap
    {
        public TripSeat tripSeat { get; set; }
    }
}
