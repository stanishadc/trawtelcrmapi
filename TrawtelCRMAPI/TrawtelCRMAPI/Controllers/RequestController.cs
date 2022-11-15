using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TrawtelCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public RequestController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllRequests()
        {
            try
            {
                var Requests = _repository.Request.GetAllRequests();
                List<RequestDTO> listRequests = new List<RequestDTO>();
                foreach (var request in Requests)
                {                    
                    listRequests.Add(getRequestDTO(request));
                }
                return Ok(listRequests);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAgents action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        private RequestDTO getRequestDTO(Request request)
        {
            RequestDTO requestDTO = new RequestDTO();
            requestDTO.RequestId = request.RequestId;
            requestDTO.TravelDate = request.TravelDate;
            requestDTO.TravelType = request.TravelType;
            requestDTO.Status = request.Status;
            requestDTO.AgentId = request.AgentId;
            var trmodel = JsonConvert.DeserializeObject<RequestRoot>(request.TravelRequest);
            requestDTO.TravelRequest = trmodel?.TravelRequest;
            requestDTO.ClientId = request.ClientId;
            requestDTO.CreatedDate = request.CreatedDate;
            request.UpdatedDate = request.UpdatedDate;
            return requestDTO;
        }
        [HttpGet("GetRequestById/{RequestId}")]
        public IActionResult GetRequestById(Guid RequestId)
        {
            try
            {
                var owner = _repository.Request.GetRequestById(RequestId);
                if (owner is null)
                {
                    _logger.LogError($"Agent with id: {RequestId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Agent with id: {RequestId}");
                    var ownerResult = getRequestDTO(owner);
                    return Ok(ownerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAgentById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("GetRequestByAgentId/{AgentId}")]
        public IActionResult GetRequestByAgentId(Guid AgentId)
        {
            try
            {
                var Requests = _repository.Request.GetRequestByAgentId(AgentId);
                if (Requests is null)
                {
                    _logger.LogError($"Agent with id: {AgentId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Agent with id: {AgentId}");
                    List<RequestDTO> listRequests = new List<RequestDTO>();
                    foreach (var request in Requests)
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
        [HttpGet("GetRequestByClientId/{ClientId}")]
        public IActionResult GetRequestByClientId(Guid ClientId)
        {
            try
            {
                var Requests = _repository.Request.GetRequestByClientId(ClientId);
                if (Requests is null)
                {
                    _logger.LogError($"Agent with id: {ClientId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Agent with id: {ClientId}");
                    List<RequestDTO> listRequests = new List<RequestDTO>();
                    foreach (var request in Requests)
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
        [HttpGet("GetRequestByTravelType/{TravelType}")]
        public IActionResult GetRequestByTravelType(char TravelType)
        {
            try
            {
                var Requests = _repository.Request.GetRequestByTravelType(TravelType);
                if (Requests is null)
                {
                    _logger.LogError($"Agent with id: {TravelType}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Agent with id: {TravelType}");
                    List<RequestDTO> listRequests = new List<RequestDTO>();
                    foreach (var request in Requests)
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
        [HttpPost]
        public IActionResult CreateRequest([FromBody] RequestDTO requestDTO)
        {
            try
            {
                if (requestDTO is null)
                {
                    _logger.LogError("request object sent from client is null.");
                    return BadRequest("request object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid request object sent from client.");
                    return BadRequest("Invalid model object");
                }
                Request request = new Request();
                request.RequestId = Guid.NewGuid();
                request.CreatedDate = DateTime.UtcNow;
                request.UpdatedDate = DateTime.UtcNow;
                request.Status = CommonEnums.Status.New.ToString();
                request.ClientId = requestDTO.ClientId;
                request.AgentId = requestDTO.AgentId;
                request.TravelType = requestDTO.TravelType;
                request.TravelDate = requestDTO.TravelDate;
                request.TravelRequest= JsonConvert.SerializeObject(new { requestDTO.TravelRequest });
                _repository.Request.CreateRequest(request);
                _repository.Save();
                return StatusCode(200, "Success");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteRequest(Guid id)
        {
            try
            {
                var Request = _repository.Request.GetRequestById(id);
                if (Request == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Request.DeleteRequest(Request);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}