using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("VisaRequests")]
    public class VisaRequest
    {
        [Key]
        public Guid VisaRequestId { get; set; }
        public Guid AgentId { get; set; }
        public Guid ClientId { get; set; }
        public Guid VisaId { get; set; }
        public DateTime TravelDate { get; set; }
        public string? Location { get; set; }
        public int NoOfApplicants { get; set; }
        public string? Applicants { get; set; }        
        public DateTime CreatedDate { get; set; }        
        public DateTime UpdatedDate { get; set; }
        public string? Status { get; set; }
        [NotMapped]
        public bool ErrorStatus { get; set; }
        [NotMapped]
        public string? ErrorMessage { get; set; }
    }
    public class VisaRequestRoot
    {
        public Location? location { get; set; }
        public List<Traveler>? travelers { get; set; }
    }
    public class VisaRequestDTO
    {
        [Key]
        public Guid VisaRequestId { get; set; }
        public Guid AgentId { get; set; }
        public Guid ClientId { get; set; }
        public Guid VisaId { get; set; }
        public DateTime TravelDate { get; set; }
        public Location? Location { get; set; }
        public int NoOfApplicants { get; set; }
        public List<Traveler>? Applicants { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? Status { get; set; }
        [NotMapped]
        public bool ErrorStatus { get; set; }
        [NotMapped]
        public string? ErrorMessage { get; set; }
    }
}
