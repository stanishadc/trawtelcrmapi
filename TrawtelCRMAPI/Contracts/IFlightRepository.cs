using Entities.Common;
using Entities.Models;

namespace Contracts
{
    public interface IFlightRepository
    {
        Response<CommonFlightsResponse> SearchFlights(FlightRequestDTO commonFlightRequest, Guid AgentId, List<AgentSuppliers>? supplierdetails);
        void CreateFlightRequest(FlightRequest commonFlightRequest);
        void UpdateFlightRequest(FlightRequest commonFlightRequest);
        void DeleteFlightRequest(FlightRequest commonFlightRequest);
        IEnumerable<FlightRequest> GetFlightRequestsByAgent(Guid AgentId);
        FlightRequest GetFlightRequestById(Guid RequestId);
        IEnumerable<FlightRequest> GetFlightRequestsByClient(Guid ClientId);
        IEnumerable<FlightRequest> GetFlightRequestsByStatus(Guid AgentId, string Status);
    }
}
