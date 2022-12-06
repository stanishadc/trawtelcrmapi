using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAirportRepository
    {
        public List<Airport> GetAirports();
        public List<Airport> SearchAirports(string searchkey);
        public Location GetAirportByCode(string airportcode);
    }
}
