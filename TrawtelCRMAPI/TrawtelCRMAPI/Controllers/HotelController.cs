using Contracts;
using Entities.Common;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using TrawtelCRMAPI.Services;

namespace TrawtelCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        HotelService _HotelService;
        private readonly IUriService uriService;
        public HotelController(ILoggerManager logger, IRepositoryWrapper repository, IUriService uriService)
        {
            _logger = logger;
            _repository = repository;
            _HotelService = new HotelService(repository);
            this.uriService = uriService;
        }      
        [HttpPost]
        public IActionResult CreateHotelRequest([FromBody] HotelRequestDTO commonHotelRequest)
        {
            try
            {
                commonHotelRequest = _HotelService.GetHotelRequestDetails(commonHotelRequest);
                if (commonHotelRequest.ErrorStatus)
                {
                    _logger.LogError($"Something went wrong inside CreateOwner action: {commonHotelRequest.ErrorMessage}");
                    return StatusCode(500, commonHotelRequest.ErrorMessage);
                }
                var response = _HotelService.SaveHotelRequest(commonHotelRequest,"Save");
                if(response.Status)
                {
                    return StatusCode(200, "Hotel Request Created");
                }
                else
                {
                    return StatusCode(400, response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, commonHotelRequest.ErrorMessage);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateHotelRequest(Guid id, [FromBody] HotelRequestDTO commonHotelRequest)
        {
            try
            {
                commonHotelRequest = _HotelService.GetHotelRequestDetails(commonHotelRequest);
                if (commonHotelRequest.ErrorStatus)
                {
                    _logger.LogError($"Something went wrong inside CreateOwner action: {commonHotelRequest.ErrorMessage}");
                    return StatusCode(500, commonHotelRequest.ErrorMessage);
                }
                var HotelEntity = _repository.Hotel.GetHotelRequestById(id);
                if (HotelEntity is null)
                {
                    _logger.LogError($"Hotel with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                var response = _HotelService.SaveHotelRequest(commonHotelRequest, "Update");
                if (response.Status)
                {
                    return StatusCode(200, "Hotel Request Updated");
                }
                else
                {
                    return StatusCode(400, response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, commonHotelRequest.ErrorMessage);
            }
        }

        [HttpGet("GetHotelRequestById/{RequestId}")]
        public IActionResult GetHotelRequestById(Guid RequestId)
        {
            try
            {
                var HotelRequest = _repository.Hotel.GetHotelRequestById(RequestId);
                if (HotelRequest is null)
                {
                    _logger.LogError($"Hotel Request with id: {RequestId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Hotel with id: {RequestId}");
                    var ownerResult = _HotelService.getRequestDTO(HotelRequest);
                    return Ok(new Response<HotelRequestDTO>(ownerResult));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetHotelRequestById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetHotelRequestsByAgent/{AgentId}")]
        public IActionResult GetHotelRequestsByAgent(Guid AgentId, [FromQuery] PaginationFilter filter)
        {
            try
            {
                var HotelRequest = _repository.Hotel.GetHotelRequestsByAgent(AgentId);
                List<HotelRequestDTO> listRequests = new List<HotelRequestDTO>();
                if (HotelRequest is null)
                {
                    _logger.LogError($"Hotel Request with id: {AgentId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Agent with id: {AgentId}");
                    foreach (var request in HotelRequest)
                    {
                        listRequests.Add(_HotelService.getRequestDTO(request));
                    }
                    var pagedResponse = _HotelService.GetPagination(listRequests, filter, Request.Path.Value, uriService);
                    return Ok(pagedResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAgentById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetHotelRequestsByClient/{ClientId}")]
        public IActionResult GetHotelRequestsByClient(Guid ClientId, [FromQuery] PaginationFilter filter)
        {
            try
            {
                var HotelRequest = _repository.Hotel.GetHotelRequestsByClient(ClientId);
                List<HotelRequestDTO> listRequests = new List<HotelRequestDTO>();
                if (HotelRequest is null)
                {
                    _logger.LogError($"Hotel Request with id: {ClientId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Agent with id: {ClientId}");
                    foreach (var request in HotelRequest)
                    {
                        listRequests.Add(_HotelService.getRequestDTO(request));
                    }
                    var pagedResponse = _HotelService.GetPagination(listRequests, filter, Request.Path.Value, uriService);
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
