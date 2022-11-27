using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class RequestDTO
    {
        [Key]
        public Guid RequestId { get; set; }
        public string? TravelType { get; set; }
        public Guid AgentId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime TravelDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Status { get; set; }
        public string? Location { get; set; }
        public string? Travelers { get; set; }
    }
}