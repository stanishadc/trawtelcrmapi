using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ClientDTO
    {
        public Guid ClientId { get; set; }
        public Guid AgentId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? ClientType { get; set; }//B2B, B2C
        public string? Status { get; set; }
    }
}
