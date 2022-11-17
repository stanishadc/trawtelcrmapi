using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("HotelRequests")]
    public class HotelRequest
    {
        [Key]
        public Guid HotelRequestId { get; set; }
        public Guid AgentId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string? Location { get; set; }
        public string? RoomDetails { get; set; }
        public string? Guests { get; set; }
        public string? HotelResponse { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedDate { get; set; }        
        public DateTime UpdatedDate { get; set; }
        [NotMapped]
        public bool ErrorStatus { get; set; }
        [NotMapped]
        public string? ErrorMessage { get; set; }
    }
    public class HotelRequestDTO
    {
        [Key]
        public Guid HotelRequestId { get; set; }
        public Guid AgentId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public Location? Location { get; set; }
        public List<RoomDetails>? RoomDetails { get; set; }
        public string? Guests { get; set; }
        public string? HotelResponse { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool ErrorStatus { get; set; }
        public string? ErrorMessage { get; set; }
    }
    public class HotelRequestRoot
    {
        public List<RoomDetails>? roomDetails { get; set; }
        public Location? location { get; set; }
    }
    public class RoomDetails
    {
        public Guid RoomDetailsId { get; set; }
        public int Adults { get; set; }
        public int[]? KidsAge { get; set; }
        public string[]? GuestDetails { get; set; }
        public string? RoomType { get; set; }
        public string? BedType { get; set; }
        public string? Breakfast { get; set; }
        public bool? SmokingRoom { get; set; }
    }
}
