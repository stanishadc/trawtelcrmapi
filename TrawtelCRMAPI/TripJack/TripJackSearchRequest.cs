using System.Collections.Generic;

namespace TripJack
{
    public class TripJackSearchRequest
    {
        public SearchQuery searchQuery { get; set; }
    }
    public class SearchQuery
    {
        public string? cabinClass { get; set; }
        public PaxInfo paxInfo { get; set; }
        public List<RouteInfos> routeInfos { get; set; }
        public SearchModifiers searchModifiers { get; set; }
    }
    public class PaxInfo
    {
        public string ADULT { get; set; }
        public string CHILD { get; set; }
        public string INFANT { get; set; }
    }
    public class RouteInfos
    {
        public FromCityOrAirport fromCityOrAirport { get; set; }
        public ToCityOrAirport toCityOrAirport { get; set; }
        public string travelDate { get; set; }
    }
    public class SearchModifiers
    {
        public bool isDirectFlight { get; set; }
        public bool isConnectingFlight { get; set; }
    }
    public class FromCityOrAirport
    {
        public string code { get; set; }
    }
    public class ToCityOrAirport
    {
        public string code { get; set; }
    }
}
