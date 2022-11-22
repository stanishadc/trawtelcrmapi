using Amazon.DynamoDBv2.DataModel;
using Amazon.S3;
using AutoMapper;
using Contracts;
using Entities;
using Entities.Common;
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
        private IMapper _mapper;
        private readonly IUriService uriService;
        public FlightController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, IAmazonS3 s3Client, IDynamoDBContext context, IUriService uriService)
        {
            _logger = logger;
            _repository = repository;
            _flightService = new FlightService(s3Client, repository);
            _context = context;
            _mapper = mapper;
            this.uriService = uriService;
        }
        [Route("Search")]
        [HttpPost]
        public APIResponse SearcAllFlights([FromBody] FlightRequestDTO commonFlightRequest)
        {
            APIResponse _objResponse = new APIResponse();
            try
            {
                commonFlightRequest = _flightService.GetFlightRequestDetails(commonFlightRequest);
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
                commonFlightRequest = _flightService.GetFlightRequestDetails(commonFlightRequest);
                if (commonFlightRequest.ErrorStatus)
                {
                    _logger.LogError($"Something went wrong inside CreateOwner action: {commonFlightRequest.ErrorMessage}");
                    return StatusCode(500, commonFlightRequest.ErrorMessage);
                }
                var response = _flightService.SaveFlightRequest(commonFlightRequest, "Save");
                if (response.Status)
                {
                    return StatusCode(200, "Flight Request Created");
                }
                else
                {
                    return StatusCode(400, response.ErrorMessage);
                }
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
                commonFlightRequest = _flightService.GetFlightRequestDetails(commonFlightRequest);
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
                var response = _flightService.SaveFlightRequest(commonFlightRequest, "Save");
                if (response.Status)
                {
                    return StatusCode(200, "Flight Request Updated");
                }
                else
                {
                    return StatusCode(400, response.ErrorMessage);
                }
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
                    var ownerResult = _flightService.getFlightRequestDTO(flightRequest);
                    return Ok(new Response<FlightRequestDTO>(ownerResult));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetFlightRequestById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetFlightRequestsByAgent/{AgentId}")]
        public IActionResult GetFlightRequestsByAgent(Guid AgentId, [FromQuery] PaginationFilter filter)
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
                        listRequests.Add(_flightService.getFlightRequestDTO(request));
                    }
                    var pagedResponse = _flightService.GetPagination(listRequests, filter, Request.Path.Value, uriService);
                    return Ok(pagedResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAgentById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetFlightRequestsByClient/{ClientId}")]
        public IActionResult GetFlightRequestsByClient(Guid ClientId, [FromQuery] PaginationFilter filter)
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
                        listRequests.Add(_flightService.getFlightRequestDTO(request));
                    }
                    var pagedResponse = _flightService.GetPagination(listRequests, filter, Request.Path.Value, uriService);
                    return Ok(pagedResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAgentById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
