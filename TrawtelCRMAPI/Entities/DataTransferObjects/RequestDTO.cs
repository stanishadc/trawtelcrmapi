using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

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
        public DateTime UpdatedDate { get; set; }
        public TravelRequest? TravelRequest { get; set; }
        public string? Status { get; set; }
    }
    public class RequestRoot
    {
        public TravelRequest? TravelRequest { get; set; }
    }
}