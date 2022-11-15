using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IFlightRepository
    {
        CommonFlightsResponse SearchFlights(FlightRequestDetails flightRequest, Guid AgentId, List<AgentSuppliers>? supplierdetails);
    }
}
