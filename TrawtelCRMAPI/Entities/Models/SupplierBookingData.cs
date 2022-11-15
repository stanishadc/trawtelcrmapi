using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class SupplierBookingData
    {
        public string SupplierId { get; set; }
        public string SupplierName { get; set; }
        public double SupplierPrice { get; set; }
        public string SessionId { get; set; }
    }
}
