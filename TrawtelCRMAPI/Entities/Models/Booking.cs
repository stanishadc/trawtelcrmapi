using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("Bookings")]
    public class Booking
    {
        [Key]
        public Guid BookingId { get; set; }
        [Required]
        public Guid AgentId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime TravelDate { get; set; }
        public string? TravelRequest { get; set; }
        public DateTime BookingDate { get; set; }
        public char TravelType { get; set; }
        public char BookingStatus { get; set; }
        public string? BookingDetails { get; set; }
        public string? TravellerDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
    public class BookingDetails
    {
        [Key]
        public Guid BookingId { get; set; }        
        public DateOnly BookingDate { get; set; }
        public PriceDetails? SalePrice { get; set; }
        public CancellationDetails? CancellationDetails { get; set; }
        public string? BookingStatus { get; set; }
        public List<TransactionDetails>? TransactionDetails { get; set; }
        public FlightBookingDetails? FlightBookingDetails { get; set; }
        public HotelBookingDetails? HotelBookingDetails { get; set; }
        public VisaBookingDetails? VisaBookingDetails { get; set; }
    }

    public class PriceDetails
    {
        [Key]
        public Guid PriceDetailsId { get; set; }
        public double NetPrice { get; set; }
        public double Tax { get; set; }
        public double TotalPrice { get; set; }
    }
    public class VisaBookingDetails
    {
        [Key]
        public Guid VisaBookingDetailsId { get; set; }
        public Location? Location { get; set; }
        public DateTime TravelDate { get; set; }
        public List<Traveler>? ApplicantDetails { get; set; }
        public PriceDetails? TotalPrice { get; set; }
        public string? VisaCopy { get; set; }
    }
    public class FlightBookingDetails
    {
        [Key]
        public Guid FlightBookingDetailsId { get; set; }
        public string? PNRNumber { get; set; }
        public Request? TravelRequest { get; set; }
        public List<Traveler>? TravellerDetails { get; set; }
        public SupplierDetails? SupplierDetails { get; set; }
    }
    public class HotelBookingDetails
    {
        public string? HotelConfirmationNumber { get; set; }
        public Request? TravelRequest { get; set; }
        public string? HotelName { get; set; }
        public int StarRating { get; set; }
        public List<RoomDetails>? RoomDetails { get; set; }
        public Location? HotelAddress { get; set; }
        public SupplierDetails? SupplierDetails { get; set; }
    }
    public class TransactionDetails
    {
        public Guid TransactionId { get; set; }
        public string? TransactionStatus { get; set; }
        public string? TransactionNumber { get; set; }
        public string[]? PaymentType { get; set; }//wallet,debitcard,creditcard,netbanking,upi,creditbalance
        public double? PaymentAmount { get; set; }
    }
    public class CancellationDetails
    {
        [Key]
        public Guid CancellationDetailsId { get; set; }
        public DateTime LastDateOfCancellation { get; set; }
        public string? CancellationPolicy { get; set; }
        public double? Charges { get; set; }
    }
    public class JourneyDetails
    {
        [Key]
        public Guid JourneyDetailsId { get; set; }
        public string? TypeOfJourney { get; set; }//onward, return
        public Location? LocationFrom { get; set; }
        public Location? LocationTo { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string? FlightNumber { get; set; }
        public string? ClassType { get; set; }
        public string? AirlineNumber { get; set; }
        public string? AirlineName { get; set; }
        public string? AirlineLogo { get; set; }
        public int Stops { get; set; }
        public TimeOnly LayOverTime { get; set; }
    }
    public class SupplierDetails
    {
        public Guid SupplierId { get; set; }
        public Guid SupplierBookingId { get; set; }
        public string? SupplierName { get; set; }
        public PriceDetails? SupplierPrice { get; set; }
    }
}
