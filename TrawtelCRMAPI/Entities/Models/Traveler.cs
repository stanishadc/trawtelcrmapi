using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("Travelers")]
    public class Traveler
    {
        [Key]
        public Guid TravelerId { get; set; }
        public Guid AgentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ProfileImage { get; set; }
        public string? Email { get; set; }
        public string? Nationality { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PassportNumber { get; set; }
        public string? PassportCountry { get; set; }
        public DateTime? PassportIssueDate { get; set; }
        public DateTime? PassportExpireDate { get; set; }
        public string? PassportFrontPage { get; set; }
        public string? PassportBackPage { get; set; }
        public string? TravelerType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
