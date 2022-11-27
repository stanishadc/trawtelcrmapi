using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Requests")]
    public class Request
    {
        [Key]
        public Guid RequestId { get; set; }
        public string? TravelType { get; set; }
        public Guid AgentId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime TravelDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? Status { get; set; }
        public string? TravelRequest { get; set; }
    }
    public class TravelRequest
    {
        public FlightRequestDetails? FlightRequestDetails { get; set; }
        public HotelRequestDetails? HotelRequestDetails { get; set; }
        public VisaRequestDetails? VisaRequestDetails { get; set; }
    }
    public class FlightRequestDetails
    {
        [Key]
        public Guid FlightRequestDetailsId { get; set; }
        public string? JourneyType { get; set; }//oneway/return/multicity
        public List<FlightJourneyRequest>? FlightJourneyRequest { get; set; }
        public int NoOfAdults { get; set; }
        public int NoOfKids { get; set; }
        public int NoOfInfants { get; set; }
        public string[]? PassengerDetails { get; set; }
        public string? ClassType { get;set; }
        public string? Error { get; set; }
    }
    
    public class HotelRequestDetails
    {
        [Key]
        public Guid HotelRequestDetailsId { get; set; }
        public List<RoomDetails>? RoomDetails { get; set; }
        public Location? Location { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
    }    
    
    public class VisaRequestDetails
    {
        [Key]
        public Guid VisaRequestDetailsId { get; set; }
        public Location? Location { get; set; }
        public DateTime TravelDate { get; set; }
        public int NoofApplicants { get; set; }
        public string[]? ApplicantDetails { get; set; }
    }
    public class Location
    {
        [Key]
        public Guid LocationId { get; set; }
        public string? AirportName { get; set; }
        public string? AirportCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
    }
}
