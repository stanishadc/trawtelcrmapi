using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Airport
    {
        public string? AirportCode { get; set; }
        public string? AirportName { get; set; }
        public string? CityCode { get; set; }
        public string? CityName { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }        
    }
}
