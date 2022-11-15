using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace TrawtelCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public AgentController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllAgents()
        {
            try
            {
                var Agents = _repository.Agent.GetAllAgents();
                var AgentsResult = _mapper.Map<IEnumerable<AgentDTO>>(Agents);
                return Ok(AgentsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAgents action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{AgentId}")]
        public IActionResult GetAgentById(Guid AgentId)
        {
            try
            {
                var owner = _repository.Agent.GetAgentById(AgentId);
                if (owner is null)
                {
                    _logger.LogError($"Agent with id: {AgentId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Agent with id: {AgentId}");
                    var ownerResult = _mapper.Map<AgentDTO>(owner);
                    return Ok(ownerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAgentById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult CreateAgent([FromBody] Agent agent)
        {
            try
            {
                if (agent is null)
                {
                    _logger.LogError("Agent object sent from client is null.");
                    return BadRequest("Agent object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Agent object sent from client.");
                    return BadRequest("Invalid model object");
                }
                agent.AgentId = Guid.NewGuid();
                agent.CreatedDate = DateTime.UtcNow;
                agent.UpdatedDate = DateTime.UtcNow;
                var agentEntity = _mapper.Map<Agent>(agent);
                _repository.Agent.CreateAgent(agentEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateAgent(Guid id, [FromBody] Agent Agent)
        {
            try
            {
                if (Agent is null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var AgentEntity = _repository.Agent.GetAgentById(id);
                if (AgentEntity is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                Agent.UpdatedDate = DateTime.UtcNow;
                Agent.CreatedDate = AgentEntity.CreatedDate;
                _mapper.Map(Agent, AgentEntity);
                _repository.Agent.UpdateAgent(AgentEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAgent(Guid id)
        {
            try
            {
                var Agent = _repository.Agent.GetAgentById(id);
                if (Agent == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Agent.DeleteAgent(Agent);
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
