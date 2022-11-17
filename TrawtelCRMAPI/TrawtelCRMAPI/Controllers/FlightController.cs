using Amazon.DynamoDBv2.DataModel;
using Amazon.S3;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TrawtelCRMAPI.Services;

namespace TrawtelCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        FlightService _flightService;
        private readonly IDynamoDBContext _context;
        public FlightController(ILoggerManager logger, IRepositoryWrapper repository, IAmazonS3 s3Client, IDynamoDBContext context)
        {
            _logger = logger;
            _repository = repository;
            _flightService = new FlightService(s3Client);
            _context = context;
        }
        [Route("Search")]
        [HttpPost]
        public APIResponse SearcAllFlights([FromBody] FlightRequestDTO commonFlightRequest)
        {
            APIResponse _objResponse = new APIResponse();
            try
            {
                commonFlightRequest = _flightService.ValidateFlightRequest(commonFlightRequest);
                if (!(bool)commonFlightRequest.ErrorStatus)
                {
                    var supplierdetails = _repository.SupplierCode.GetDefaultSupplierByAgentId(commonFlightRequest.AgentId, CommonEnums.TravelType.Flight.ToString());
                    if (supplierdetails.Count > 0)
                    {
                        var flightsResponse = _repository.Flight.SearchFlights(commonFlightRequest, commonFlightRequest.AgentId, supplierdetails);
                        if ((bool)flightsResponse.status)
                        {
                            var flightsData = flightsResponse.commonFlightDetails;
                            if (flightsData?.Count > 0)
                            {
                                flightsData = _flightService.CustomizeFlights(flightsData);

                                _objResponse.Data = flightsData;
                                _objResponse.Status = true;
                                return _objResponse;
                            }
                            else
                            {
                                _objResponse.ErrorMessage = "No Flights Found";
                                _objResponse.Status = false;
                                return _objResponse;
                            }
                        }
                        else
                        {
                            _objResponse.ErrorMessage = flightsResponse.ErrorMessage;
                            _objResponse.Status = false;
                            return _objResponse;
                        }
                    }
                    else
                    {
                        _objResponse.ErrorMessage = "No Active Supplier";
                        _objResponse.Status = false;
                        return _objResponse;
                    }
                }
                else
                {
                    _objResponse.ErrorMessage = commonFlightRequest.ErrorMessage;
                    _objResponse.Status = false;
                    return _objResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception occurred in the Flight Search Request : " + ex);
                _objResponse.ErrorMessage = "Error in Search Request";
                _objResponse.Status = false;
                return _objResponse;
            }
        }
        
        [HttpPost]
        public IActionResult CreateFlightRequest([FromBody] FlightRequestDTO commonFlightRequest)
        {
            try
            {
                commonFlightRequest = _flightService.ValidateFlightRequest(commonFlightRequest);
                if (commonFlightRequest.ErrorStatus)
                {
                    _logger.LogError($"Something went wrong inside CreateOwner action: {commonFlightRequest.ErrorMessage}");
                    return StatusCode(500, commonFlightRequest.ErrorMessage);
                }
                FlightRequest flightRequest = new FlightRequest();
                flightRequest.FlightRequestId = Guid.NewGuid();
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

                _repository.Flight.CreateFlightRequest(flightRequest);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, commonFlightRequest.ErrorMessage);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFlightRequest(Guid id, [FromBody] FlightRequestDTO commonFlightRequest)
        {
            try
            {
                commonFlightRequest = _flightService.ValidateFlightRequest(commonFlightRequest);
                if (commonFlightRequest.ErrorStatus)
                {
                    _logger.LogError($"Something went wrong inside CreateOwner action: {commonFlightRequest.ErrorMessage}");
                    return StatusCode(500, commonFlightRequest.ErrorMessage);
                }
                var FlightEntity = _repository.Flight.GetFlightRequestById(id);
                if (FlightEntity is null)
                {
                    _logger.LogError($"Flight with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                FlightRequest flightRequest = new FlightRequest();
                flightRequest.FlightRequestId = id;
                flightRequest.AgentId = commonFlightRequest.AgentId;
                flightRequest.ClientId = commonFlightRequest.ClientId;
                flightRequest.Adults = commonFlightRequest.Adults;
                flightRequest.CabinClass = commonFlightRequest.CabinClass;
                flightRequest.Infants = commonFlightRequest.Infants;
                flightRequest.JourneyType = commonFlightRequest.JourneyType;
                flightRequest.Kids = commonFlightRequest.Kids;
                flightRequest.Status = CommonEnums.Status.Replied.ToString();
                flightRequest.CreatedDate = FlightEntity.CreatedDate;
                flightRequest.UpdatedDate = DateTime.UtcNow;
                if (commonFlightRequest.flightJourneyRequest != null)
                {
                    if (commonFlightRequest.flightJourneyRequest.Count > 0)
                    {
                        flightRequest.TravelDate = commonFlightRequest.flightJourneyRequest[0].DepartureDate;
                    }
                }
                flightRequest.flightJourneyRequest = JsonConvert.SerializeObject(new { commonFlightRequest.flightJourneyRequest });

                _repository.Flight.UpdateFlightRequest(flightRequest);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, commonFlightRequest.ErrorMessage);
            }
        }

        [HttpGet("GetFlightRequestById/{RequestId}")]
        public IActionResult GetFlightRequestById(Guid RequestId)
        {
            try
            {
                var flightRequest = _repository.Flight.GetFlightRequestById(RequestId);
                if (flightRequest is null)
                {
                    _logger.LogError($"Flight Request with id: {RequestId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned flight with id: {RequestId}");
                    var ownerResult = getRequestDTO(flightRequest);
                    return Ok(ownerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetFlightRequestById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetFlightRequestsByAgent/{AgentId}")]
        public IActionResult GetFlightRequestsByAgent(Guid AgentId)
        {
            try
            {
                var flightRequest = _repository.Flight.GetFlightRequestsByAgent(AgentId);
                List<FlightRequestDTO> listRequests = new List<FlightRequestDTO>();
                if (flightRequest is null)
                {
                    _logger.LogError($"Flight Request with id: {AgentId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Agent with id: {AgentId}");                    
                    foreach (var request in flightRequest)
                    {
                        listRequests.Add(getRequestDTO(request));
                    }
                    return Ok(listRequests);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAgentById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetFlightRequestsByClient/{ClientId}")]
        public IActionResult GetFlightRequestsByClient(Guid ClientId)
        {
            try
            {
                var flightRequest = _repository.Flight.GetFlightRequestsByClient(ClientId);
                List<FlightRequestDTO> listRequests = new List<FlightRequestDTO>();
                if (flightRequest is null)
                {
                    _logger.LogError($"Flight Request with id: {ClientId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Agent with id: {ClientId}");
                    foreach (var request in flightRequest)
                    {
                        listRequests.Add(getRequestDTO(request));
                    }
                    return Ok(listRequests);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAgentById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        private FlightRequestDTO getRequestDTO(FlightRequest flightRequest)
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

            return flightRequestDTO;
        }
    }
}
