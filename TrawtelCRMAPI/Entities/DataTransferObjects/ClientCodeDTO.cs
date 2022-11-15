using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class SupplierCodeDTO
    {
        public Guid SupplierCodeId { get; set; }
        public Guid AgentId { get; set; }
        public Guid SupplierId { get; set; }
        public string? TestAPI { get; set; }
        public string? TestUserName { get; set; }
        public string? TestPassword { get; set; }
        public string? Status { get; set; }
    }
}
