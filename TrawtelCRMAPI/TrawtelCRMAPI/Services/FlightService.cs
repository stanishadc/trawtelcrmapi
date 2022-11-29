using Amazon.DynamoDBv2.DataModel;
using Amazon.S3;
using Contracts;
using Entities;
using Entities.Common;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TrawtelCRMAPI.DynamoDB;
using TrawtelCRMAPI.Helpers;

namespace TrawtelCRMAPI.Services
{
    public class FlightService
    {
        ValidationService _validationService = new ValidationService();
        TravelerService _travelerService;
        private readonly AmazonService _amazonService;
        private IRepositoryWrapper _repository;
        private readonly IDynamoDBContext _context;
        DynamoDBService dBService;
        public FlightService(IAmazonS3 s3Client, IRepositoryWrapper repository, IDynamoDBContext context)
        {
            _amazonService = new AmazonService(s3Client);
            _repository = repository;
            _travelerService = new TravelerService(_repository);
            _context = context;
            dBService = new DynamoDBService(_context);
        }
        public FlightRequestDTO GetFlightRequestDetails(FlightRequestDTO flightRequestDTO)
        {
            flightRequestDTO = _validationService.ValidateFlightRequest(flightRequestDTO);
            if (flightRequestDTO.ErrorStatus)
            {
                return flightRequestDTO;
            }

            return flightRequestDTO;
        }
        public Response<CommonFlightsResponse> SearchFlights(FlightRequestDTO commonFlightRequest, List<AgentSuppliers> supplierdetails)
        {
            Response<CommonFlightsResponse> response = new Response<CommonFlightsResponse>();
            CommonFlightsResponse commonFlights = new CommonFlightsResponse();
            var flightsResponse = _repository.Flight.SearchFlights(commonFlightRequest, commonFlightRequest.AgentId, supplierdetails);
            if (flightsResponse.Succeeded)
            {
                var flightsData = flightsResponse.Data.commonFlightDetails;
                if (flightsData?.Count > 0)
                {
                    flightsData = CustomizeFlights(flightsData);
                    //SaveDataInDynamoDB(flightsData);
                    commonFlights.commonFlightDetails = flightsData;
                    response.Succeeded = true;
                    response.Data = commonFlights;
                    return response;
                }
                else
                {
                    response.Message = "No Flights Found";
                    response.Succeeded = false;
                    return response;
                }
            }
            else
            {
                response.Message = flightsResponse.Message;
                response.Succeeded = false;
                return response;
            }
        }
        public Response<CommonFlightsResponse> GetFlightDetails(List<CommonFlightDetails> commonFlightDetails)
        {
            Response<CommonFlightsResponse> response = new Response<CommonFlightsResponse>();
            if (commonFlightDetails.Count > 0)
            {
                return response;
            }
            else
            {
                response.Message = "Please select itinerary";
                response.Succeeded = false;
                return response;
            }
        }
        private bool SaveDataInDynamoDB(List<CommonFlightDetails> flightsData)
        {
            dBService.SaveSearchData(flightsData);
            return true;
        }

        public List<CommonFlightDetails> CustomizeFlights(List<CommonFlightDetails> commonFlightDetailsList)
        {
            //commonFlightDetailsList = getAirlineLogos(commonFlightDetailsList);
            commonFlightDetailsList = FilterFlights(commonFlightDetailsList);
            commonFlightDetailsList = SortFlights(commonFlightDetailsList);
            return commonFlightDetailsList;
        }

        private List<CommonFlightDetails> getAirlineLogos(List<CommonFlightDetails> commonFlightDetailsList)
        {
            for (int i = 0; i < commonFlightDetailsList.Count; i++)
            {
                for (int k = 0; k < commonFlightDetailsList[i].tFSegments.Count; k++)
                {
                    commonFlightDetailsList[i].tFSegments[k].AirlineLogo = _amazonService.getAirportlogo(commonFlightDetailsList[i].tFSegments[k].AirlineCode);
                }
            }
            return commonFlightDetailsList;
        }
        public List<CommonFlightDetails> FilterFlights(List<CommonFlightDetails> commonFlightDetailsList)
        {
            return commonFlightDetailsList;
        }
        public List<CommonFlightDetails> SortFlights(List<CommonFlightDetails> commonFlightDetailsList)
        {
            //remove duplicates
            //commonFlightDetailsList = (List<CommonFlightDetails>)commonFlightDetailsList.Distinct();
            return commonFlightDetailsList;
        }
        public FlightRequestDTO getFlightRequestDTO(FlightRequest flightRequest)
        {
            FlightRequestDTO flightRequestDTO = new FlightRequestDTO();
            flightRequestDTO.FlightRequestId = flightRequest.FlightRequestId;
            flightRequestDTO.AgentId = flightRequest.AgentId;
            flightRequestDTO.ClientId = flightRequest.ClientId;
            flightRequestDTO.Adults = flightRequest.Adults;
            flightRequestDTO.CabinClass = flightRequest.CabinClass;
            flightRequestDTO.Infants = flightRequest.Infants;
            flightRequestDTO.JourneyType = flightRequest.JourneyType;
            flightRequestDTO.Kids = flightRequest.Kids;
            flightRequestDTO.Status = flightRequest.Status;
            flightRequestDTO.CreatedDate = flightRequest.CreatedDate;
            flightRequestDTO.UpdatedDate = flightRequest.UpdatedDate;
            flightRequestDTO.TravelDate = flightRequest.TravelDate;

            var trmodel = JsonConvert.DeserializeObject<FlightRequestRoot>(flightRequest.flightJourneyRequest);
            flightRequestDTO.flightJourneyRequest = trmodel?.flightJourneyRequest;
            var travelers = JsonConvert.DeserializeObject<string[]>(flightRequest.Passengers);
            if (travelers.Length > 0)
            {
                flightRequestDTO.Passengers = _travelerService.GetApplicants(travelers);
            }
            return flightRequestDTO;
        }
        public Response<object> SaveFlightRequest(FlightRequestDTO commonFlightRequest, string QueryType)
        {
            Response<object> _apiResponse = new Response<object>();
            try
            {
                FlightRequest flightRequest = new FlightRequest();

                flightRequest.AgentId = commonFlightRequest.AgentId;
                flightRequest.ClientId = commonFlightRequest.ClientId;
                flightRequest.Adults = commonFlightRequest.Adults;
                flightRequest.CabinClass = commonFlightRequest.CabinClass;
                flightRequest.Infants = commonFlightRequest.Infants;
                flightRequest.JourneyType = commonFlightRequest.JourneyType;
                flightRequest.Kids = commonFlightRequest.Kids;
                flightRequest.Status = CommonEnums.Status.New.ToString();
                flightRequest.CreatedDate = DateTime.UtcNow;
                flightRequest.UpdatedDate = DateTime.UtcNow;
                if (commonFlightRequest.flightJourneyRequest != null)
                {
                    if (commonFlightRequest.flightJourneyRequest.Count > 0)
                    {
                        flightRequest.TravelDate = commonFlightRequest.flightJourneyRequest[0].DepartureDate;
                    }
                }
                flightRequest.flightJourneyRequest = JsonConvert.SerializeObject(new { commonFlightRequest.flightJourneyRequest });
                var apiresponse = _travelerService.ConvertTravelerToStringArray(commonFlightRequest.Passengers);
                if (apiresponse.Succeeded)
                {
                    flightRequest.Passengers = JsonConvert.SerializeObject((string[]?)apiresponse.Data);
                }
                else
                {
                    return apiresponse;
                }
                if (QueryType == "Save")
                {
                    flightRequest.FlightRequestId = Guid.NewGuid();
                    flightRequest.Status = CommonEnums.Status.New.ToString();
                    flightRequest.CreatedDate = DateTime.UtcNow;
                    flightRequest.UpdatedDate = DateTime.UtcNow;
                    _repository.Flight.CreateFlightRequest(flightRequest);
                    _apiResponse.Message = "Flight Request Created";
                }
                else
                {
                    flightRequest.FlightRequestId = commonFlightRequest.FlightRequestId;
                    flightRequest.Status = CommonEnums.Status.Replied.ToString();
                    flightRequest.UpdatedDate = DateTime.UtcNow;
                    _repository.Flight.UpdateFlightRequest(flightRequest);
                    _apiResponse.Message = "Flight Request Updated";
                }
                _repository.Save();
                _apiResponse.Succeeded = true;
            }
            catch (Exception ex)
            {
                _apiResponse.Succeeded = false;
                _apiResponse.Message = ex.Message;
            }
            return _apiResponse;
        }
        internal object GetPagination(List<FlightRequestDTO> listRequests, PaginationFilter filter, string? route, IUriService uriService)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = listRequests.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToList();
            var totalRecords = listRequests.Count;
            var pagedReponse = PaginationHelper.CreatePagedReponse<FlightRequestDTO>(pagedData, validFilter, totalRecords, uriService, route);
            return pagedReponse;
        }
        internal object GetFlightSearchPagination(List<CommonFlightDetails> listRequests, PaginationFilter filter, string? route, IUriService uriService, FlightRequestDTO flightRequestDTO)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = listRequests.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToList();
            var totalRecords = listRequests.Count;
            var pagedReponse = PaginationHelper.CreateFlightPagedReponse<CommonFlightDetails>(pagedData, validFilter, totalRecords, uriService, route, flightRequestDTO );
            return pagedReponse;
        }        
    }
}
