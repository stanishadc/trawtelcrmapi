using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class APIResponse
    {
        public string ErrorMessage { get; set; }
        public bool Status { get; set; }
        public object Data { get; set; }
    }
}
