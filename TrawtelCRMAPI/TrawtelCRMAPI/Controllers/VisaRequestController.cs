using Contracts;
using Entities.Common;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrawtelCRMAPI.Services;

namespace TrawtelCRMAPI.Controllers
{
    [Authorize(Roles = "Agent")]
    [Route("api/[controller]")]
    [ApiController]
    public class VisaRequestController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        VisaService _visaService;
        private readonly IUriService uriService;
        public VisaRequestController(ILoggerManager logger, IRepositoryWrapper repository, IUriService uriService)
        {
            _logger = logger;
            _repository = repository;
            _visaService = new VisaService(repository);
            this.uriService = uriService;
        }
        [HttpPost]
        public IActionResult CreateVisaRequest([FromBody] VisaRequestDTO commonVisaRequest)
        {
            try
            {
                commonVisaRequest = _visaService.GetVisaRequestDetails(commonVisaRequest);
                if (commonVisaRequest.ErrorStatus)
                {
                    _logger.LogError($"Something went wrong inside CreateOwner action: {commonVisaRequest.ErrorMessage}");
                    return StatusCode(500, commonVisaRequest.ErrorMessage);
                }
                var response = _visaService.SaveVisaRequest(commonVisaRequest, "Save");
                if (response.Succeeded)
                {
                    return StatusCode(200, response);
                }
                else
                {
                    return StatusCode(400, response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, commonVisaRequest.ErrorMessage);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateVisaRequest(Guid id, [FromBody] VisaRequestDTO commonVisaRequest)
        {
            try
            {
                commonVisaRequest = _visaService.GetVisaRequestDetails(commonVisaRequest);
                if (commonVisaRequest.ErrorStatus)
                {
                    _logger.LogError($"Something went wrong inside CreateOwner action: {commonVisaRequest.ErrorMessage}");
                    return StatusCode(500, commonVisaRequest.ErrorMessage);
                }
                var VisaEntity = _repository.VisaRequest.GetVisaRequestById(id);
                if (VisaEntity is null)
                {
                    _logger.LogError($"Visa with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                var response = _visaService.SaveVisaRequest(commonVisaRequest, "Update");
                if (response.Succeeded)
                {
                    return StatusCode(200, response);
                }
                else
                {
                    return StatusCode(400, response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, commonVisaRequest.ErrorMessage);
            }
        }

        [HttpGet("GetVisaRequestById/{RequestId}")]
        public IActionResult GetVisaRequestById(Guid RequestId)
        {
            try
            {                
                var VisaRequest = _repository.VisaRequest.GetVisaRequestById(RequestId);
                if (VisaRequest is null)
                {
                    _logger.LogError($"Visa Request with id: {RequestId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Visa with id: {RequestId}");
                    var ownerResult = _visaService.getRequestDTO(VisaRequest);
                    return Ok(new Response<VisaRequestDTO>(ownerResult));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVisaRequestById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetVisaRequestsByAgent/{AgentId}")]
        public IActionResult GetVisaRequestsByAgent(Guid AgentId, [FromQuery] PaginationFilter filter)
        {
            try
            {
                var VisaRequest = _repository.VisaRequest.GetVisaRequestsByAgent(AgentId);                
                List<VisaRequestDTO> listRequests = new List<VisaRequestDTO>();
                if (VisaRequest is null)
                {
                    _logger.LogError($"Visa Request with id: {AgentId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Agent with id: {AgentId}");
                    foreach (var request in VisaRequest)
                    {
                        listRequests.Add(_visaService.getRequestDTO(request));
                    }
                    var pagedResponse = _visaService.GetPagination(listRequests, filter, Request.Path.Value, uriService);
                    return Ok(pagedResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAgentById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("GetVisaRequestsByClient/{ClientId}")]
        public IActionResult GetVisaRequestsByClient(Guid ClientId, [FromQuery] PaginationFilter filter)
        {
            try
            {
                var VisaRequest = _repository.VisaRequest.GetVisaRequestsByClient(ClientId);
                List<VisaRequestDTO> listRequests = new List<VisaRequestDTO>();
                if (VisaRequest is null)
                {
                    _logger.LogError($"Visa Request with id: {ClientId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Agent with id: {ClientId}");
                    foreach (var request in VisaRequest)
                    {
                        listRequests.Add(_visaService.getRequestDTO(request));
                    }
                    var pagedResponse = _visaService.GetPagination(listRequests, filter, Request.Path.Value, uriService);
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
