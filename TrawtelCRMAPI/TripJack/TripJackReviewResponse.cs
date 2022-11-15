using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripJack
{
    public class TripJackReviewResponse
    {
        public List<TripInfo> tripInfos { get; set; }
        public SearchQuery searchQuery { get; set; }
        public string bookingId { get; set; }
        public TotalPriceInfo totalPriceInfo { get; set; }
        public Status status { get; set; }
        public Conditions conditions { get; set; }
        public MetaInfo metaInfo { get; set; }
    }
    public class MEAL
    {
        public string code { get; set; }
        public double amount { get; set; }
        public string desc { get; set; }
    }
    public class BAGGAGE
    {
        public string code { get; set; }
        public double amount { get; set; }
        public string desc { get; set; }
    }
    public class SsrInfo
    {
        public List<MEAL> MEAL { get; set; }
        public List<BAGGAGE> BAGGAGE { get; set; }
    }
    public class Pc
    {
        public string code { get; set; }
        public string name { get; set; }
        public bool isLcc { get; set; }
    }
    public class TripInfo
    {
        public List<SI> sI { get; set; }
        public List<TotalPriceList> totalPriceList { get; set; }
    }
    public class RouteInfo
    {
        public FromCityOrAirport fromCityOrAirport { get; set; }
        public ToCityOrAirport toCityOrAirport { get; set; }
        public string travelDate { get; set; }
    }
    public class TotalFareDetail
    {
        public FC fC { get; set; }
        public AfC afC { get; set; }
    }
    public class TotalPriceInfo
    {
        public TotalFareDetail totalFareDetail { get; set; }
    }
    public class Dob
    {
        public bool adobr { get; set; }
        public bool cdobr { get; set; }
        public bool idobr { get; set; }
    }

    public class Gst
    {
        public bool gstappl { get; set; }
        public bool igm { get; set; }
    }

    public class Conditions
    {
        public List<object> ffas { get; set; }
        public bool isa { get; set; }
        public Dob dob { get; set; }
        public bool iecr { get; set; }
        public bool isBA { get; set; }
        public int st { get; set; }
        public DateTime sct { get; set; }
        public Gst gst { get; set; }
    }
}
