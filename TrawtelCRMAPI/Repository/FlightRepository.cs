using Contracts;
using Entities;
using Entities.Models;
using System.Reflection;
using TripJack;
using static Entities.CommonEnums;

namespace Repository
{
    public class FlightRepository : RepositoryBase<FlightRequest>, IFlightRepository
    {
        RepositoryContext _repositoryContext;
        TripJackProxy _tripJackProxy;
        public FlightRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _tripJackProxy = new TripJackProxy();
        }
        public CommonFlightsResponse SearchFlights(FlightRequestDTO commonFlightRequest, Guid AgentId, List<AgentSuppliers>? supplierdetails)
        {
            CommonFlightsResponse commonFlightsResponse = new CommonFlightsResponse();
            Parallel.ForEach(supplierdetails, AS =>
                {
                    if (AS.SupplierName == SupplierNames.TripJack.ToString())
                    {
                        commonFlightsResponse = _tripJackProxy.CreateSearchRequest(commonFlightRequest, AS);
                    }
                });
            return commonFlightsResponse;
        }        
        public IEnumerable<FlightRequest> GetFlightRequestsByAgent(Guid AgentId)
        {
            return FindAll()
                .OrderBy(ow => ow.CreatedDate)
                .ToList();
        }
        public FlightRequest GetFlightRequestById(Guid FlightRequestId)
        {
            return FindByCondition(client => client.FlightRequestId.Equals(FlightRequestId)).FirstOrDefault();
        }
        public IEnumerable<FlightRequest> GetFlightRequestsByClient(Guid ClientId)
        {
            return FindByCondition(client => client.ClientId.Equals(ClientId)).ToList();
        }
        public void CreateFlightRequest(FlightRequest commonFlightRequest)
        {
            Create(commonFlightRequest);
        }
        public void UpdateFlightRequest(FlightRequest commonFlightRequest)
        {
            Update(commonFlightRequest);
        }
        public void DeleteFlightRequest(FlightRequest commonFlightRequest)
        {
            Delete(commonFlightRequest);
        }
    }
}
