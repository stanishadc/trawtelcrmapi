using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("FlightRequests")]
    public class FlightRequest
    {
        [Key]
        public Guid FlightRequestId { get; set; }
        public Guid AgentId { get; set; }
        public Guid ClientId { get; set; }
        public string? JourneyType { get; set; }
        public int Adults { get; set; }
        public int Kids { get; set; }
        public int Infants { get; set; }
        public string? flightJourneyRequest { get; set; }
        public string? Passengers { get; set; }
        public string? CabinClass { get; set; }
        public DateTime TravelDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Status { get; set; }
        public DateTime UpdatedDate { get; set; }
        [NotMapped]
        public bool ErrorStatus { get; set; }
        [NotMapped]
        public string? ErrorMessage { get; set; }
    }
    public class FlightRequestRoot
    {
        public List<FlightJourneyRequest>? flightJourneyRequest { get; set; }
    }
    public class FlightRequestDTO
    {
        [Key]
        public Guid FlightRequestId { get; set; }
        public Guid AgentId { get; set; }
        public Guid ClientId { get; set; }
        public string? JourneyType { get; set; }
        public int Adults { get; set; }
        public int Kids { get; set; }
        public int Infants { get; set; }
        public List<FlightJourneyRequest>? flightJourneyRequest { get; set; }
        public List<Traveler>? Passengers { get; set; }
        public string? CabinClass { get; set; }
        public DateTime TravelDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Status { get; set; }
        public DateTime UpdatedDate { get; set; }
        [NotMapped]
        public bool ErrorStatus { get; set; }
        [NotMapped]
        public string? ErrorMessage { get; set; }
    }
    public class FlightJourneyRequest
    {
        [Key]
        public Guid FlightJourneyRequestId { get; set; }
        public string? TypeOfJourney { get; set; }//onward, return
        public Location? LocationFrom { get; set; }
        public Location? LocationTo { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
    public class AgentSuppliers
    {
        public Guid SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierKey { get; set; }
        public string? SupplierUserName { get; set; }
        public string? SupplierPassword { get; set; }
        public string? SupplierURL { get; set; }
    }
    public class CommonFlightsResponse
    {
        public FlightRequestDTO? commonFlightRequest { get; set; }
        public List<CommonFlightDetails>? commonFlightDetails { get; set; }
        //public bool? status { get; set; }
        //public string? ErrorMessage{get;set;}
    }
    public class CommonFlightDetails
    {
        public string? JourneyType { get; set; }
        public Guid TFId { get; set; }
        public Guid SupplierId { get; set; }
        public string? SupplierLegId { get; set; }
        public string? SupplierSessionId { get; set; }
        public TFDepartureData? tFDepartureData { get; set; }
        public TFArrivalData? tFArrivalData { get; set; }
        public int Stops { get; set; }
        public bool MealType { get; set; }
        public int NoOfSeats { get; set; }
        public int RefundType { get; set; }
        public List<TFSegments>? tFSegments { get; set; }
        public TFPriceDetails? tFPriceDetails { get; set; }        
    }
    public class TFSegments
    {
        public TFDepartureData? tFDepartureData { get; set; }
        public TFArrivalData? tFArrivalData { get; set; }
        public string? SupplierSegmentId { get; set; }
        public string? Airline { get; set; }
        public string? AirlineCode { get; set; }
        public string? AirlineLogo { get; set; }
        public string? EquimentType { get; set; }        
        public string? FlightNumber { get; set; }
        public int Duration { get; set; }
        public int ConnectingTime { get; set; }
    }
    public class TFPriceDetails
    {
        public TFSupplierPrice? tFSupplierPrice { get; set; }
        public TFAgentPrice? tFAgentPrice { get; set; }
    }
    public class TFSupplierPrice
    {
        public TFPassengerPrice? tFAdults { get; set; }
        public TFPassengerPrice? tFKids { get; set; }
        public TFPassengerPrice? tFInfants { get; set; }
        public double TotalPrice { get; set; }
    }
    public class TFAgentPrice
    {
        public TFPassengerPrice? tFAdults { get; set; }
        public TFPassengerPrice? tFKids { get; set; }
        public TFPassengerPrice? tFInfants { get; set; }        
        public double TotalPrice { get; set; }
    }
    public class TFPassengerPrice
    {
        public double BaseFare { get; set; }
        public double NetFare { get; set; }
        public double Tax { get; set; }
        public double TotalFare { get; set; }
        public string? MealType { get; set; }
        public string? Refundable { get; set; }
        public string? CabinBag { get; set; }
        public string? CheckinBag { get; set; }
        public string? CabinClass { get; set; }
        public TFOtherCharges? tFOtherCharges { get; set; }
    }
    public class TFOtherCharges
    {
        public double OtherCharges { get; set; }
        public double ManagementFee { get; set; }
        public double ManagementFeeTax { get; set; }
        public double AirlineGST { get; set; }
        public double FuelSurcharge { get; set; }
        public double? CarrierMiscFee { get; set; }
    }
    public class TFDepartureData
    {
        public DateTime? DepartureDateTime { get; set; }
        public string? AirportCode { get; set; }
        public string? AirportName { get; set; }
        public string? CityCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? CountryCode { get; set; }
        public string? Terminal { get; set; }
    }
    public class TFArrivalData
    {
        public DateTime? ArrivalDateTime { get; set; }
        public string? AirportCode { get; set; }
        public string? AirportName { get; set; }
        public string? CityCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? CountryCode { get; set; }
        public string? Terminal { get; set; }
    }
}
