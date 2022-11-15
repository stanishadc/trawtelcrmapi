using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class SupplierDTO
    {
        public Guid SupplierId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Expertise { get; set; }//B2B, B2C
        public bool? API { get; set; }
        public string? Status { get; set; }
    }
}
