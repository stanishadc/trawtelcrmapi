using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("SupplierCodes")]
    public class SupplierCode
    {
        [Key]
        public Guid SupplierCodeId { get; set; }
        public string? TravelType { get; set; }
        public string? TestAPIKey { get; set; }

        public string? TestUserName { get; set; }

        public string? TestPassword { get; set; }

        public string? TestURL { get; set; }

        public string? LiveAPIKey { get; set; }

        public string? LiveUserName { get; set; }

        public string? LivePassword { get; set; }

        public string? LiveURL { get; set; }

        public string? Status { get; set; }
        public Guid AgentId { get; set; }
        public Guid SupplierId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
