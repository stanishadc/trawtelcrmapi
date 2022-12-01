using AutoMapper;
using Contracts;
using Entities.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TrawtelCRMAPI.Services;

namespace TrawtelCRMAPI.Controllers
{
    [Authorize(Roles = "Agent")]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        RequestService _requestService;
        private readonly IUriService uriService;
        public RequestController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, IUriService uriService)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _requestService = new RequestService(repository);
            this.uriService = uriService;
        }
        [HttpGet("{AgentId}")]
        public IActionResult GetRequests(Guid AgentId, [FromQuery] PaginationFilter filter)
        
        {
            try
            {
                var listRequests = _requestService.getRequests(AgentId);
                var pagedResponse = _requestService.GetPagination(listRequests, filter, Request.Path.Value, uriService);
                return Ok(pagedResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAgents action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Status")]
        public IActionResult GetRequestsByStatus(Guid AgentId,string Status, [FromQuery] PaginationFilter filter)
        {
            try
            {
                var listRequests = _requestService.getRequestByStatus(AgentId, Status);
                var pagedResponse = _requestService.GetPagination(listRequests, filter, Request.Path.Value, uriService);
                return Ok(pagedResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAgents action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}