using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAirlineRepository
    {
        public List<Airline> GetAirlines();
        public List<Airline> SearchAirlines(string searchkey);
    }
}
