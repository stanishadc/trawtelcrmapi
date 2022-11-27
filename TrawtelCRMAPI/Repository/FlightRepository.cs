using Contracts;
using Entities;
using Entities.Common;
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
        public Response<CommonFlightsResponse> SearchFlights(FlightRequestDTO commonFlightRequest, Guid AgentId, List<AgentSuppliers>? supplierdetails)
        {
            Response<CommonFlightsResponse> commonFlightsResponse = new Response<CommonFlightsResponse>();
            CommonFlightsResponse commonFlights = new CommonFlightsResponse();

            commonFlights.commonFlightRequest = commonFlightRequest;
            List<CommonFlightDetails> commonFlightDetailsList = new List<CommonFlightDetails>();
            Parallel.ForEach(supplierdetails, AS =>
                {
                    if (AS.SupplierName == SupplierNames.TripJack.ToString())
                    {
                        List<CommonFlightDetails> tripjackFlightDetailsList = new List<CommonFlightDetails>();
                        tripjackFlightDetailsList = _tripJackProxy.CreateSearchRequest(commonFlightRequest, AS, tripjackFlightDetailsList);
                        commonFlightDetailsList.AddRange(tripjackFlightDetailsList);
                    }
                });
            commonFlights.commonFlightDetails = commonFlightDetailsList;
            commonFlightsResponse.Data = commonFlights;
            commonFlightsResponse.Succeeded = true;
            return commonFlightsResponse;
        }
        public Response<CommonFlightsResponse> GetFlightDetails(FlightRequestDTO commonFlightRequest, Guid AgentId, List<AgentSuppliers>? supplierdetails)
        {
            Response<CommonFlightsResponse> commonFlightsResponse = new Response<CommonFlightsResponse>();
            CommonFlightsResponse commonFlights = new CommonFlightsResponse();

            commonFlights.commonFlightRequest = commonFlightRequest;
            List<CommonFlightDetails> commonFlightDetailsList = new List<CommonFlightDetails>();
            Parallel.ForEach(supplierdetails, AS =>
            {
                if (AS.SupplierName == SupplierNames.TripJack.ToString())
                {
                    List<CommonFlightDetails> tripjackFlightDetailsList = new List<CommonFlightDetails>();
                    tripjackFlightDetailsList = _tripJackProxy.CreateSearchRequest(commonFlightRequest, AS, tripjackFlightDetailsList);
                    commonFlightDetailsList.AddRange(tripjackFlightDetailsList);
                }
            });
            commonFlights.commonFlightDetails = commonFlightDetailsList;
            commonFlightsResponse.Data = commonFlights;
            commonFlightsResponse.Succeeded = true;
            return commonFlightsResponse;
        }
        public IEnumerable<FlightRequest> GetFlightRequestsByStatus(Guid AgentId, string Status)
        {
            return FindByCondition(client => client.AgentId.Equals(AgentId) && client.Status.Equals(Status)).ToList();
        }
        public IEnumerable<FlightRequest> GetFlightRequestsByAgent(Guid AgentId)
        {
            return FindByCondition(client => client.AgentId.Equals(AgentId)).ToList();
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
