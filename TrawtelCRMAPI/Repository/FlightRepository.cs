using Contracts;
using Entities;
using Entities.Models;
using System.Reflection;
using TripJack;
using static Entities.CommonEnums;

namespace Repository
{
    public class FlightRepository : RepositoryBase<SupplierCode>, IFlightRepository
    {
        RepositoryContext _repositoryContext;
        TripJackProxy _tripJackProxy;
        public FlightRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _tripJackProxy = new TripJackProxy();
        }
        public CommonFlightsResponse SearchFlights(FlightRequestDetails flightRequest, Guid AgentId, List<AgentSuppliers>? supplierdetails)
        {
            CommonFlightRequest commonFlightRequest = new CommonFlightRequest();
            commonFlightRequest.Adults = flightRequest.NoOfAdults;
            commonFlightRequest.Infants = flightRequest.NoOfInfants;
            commonFlightRequest.Kids = flightRequest.NoOfKids;
            commonFlightRequest.CabinClass = flightRequest.ClassType;
            commonFlightRequest.JourneyType = flightRequest.JourneyType;
            commonFlightRequest.flightJourneyRequest = flightRequest.FlightJourneyRequest;
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
    }
}
