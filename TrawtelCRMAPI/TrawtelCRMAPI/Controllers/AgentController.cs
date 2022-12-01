using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Agent")]
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
        [Authorize(Roles = "Agent")]
        [HttpGet("GetAgentByUserId/{UserId}")]
        public IActionResult GetAgentByUserId(Guid UserId)
        {
            try
            {
                var owner = _repository.Agent.GetAgentByUserId(UserId);
                if (owner is null)
                {
                    _logger.LogError($"Agent with id: {UserId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Agent with id: {UserId}");
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
        public IActionResult CreateAgent([FromBody] AgentDTO agentDTO)
        {
            try
            {
                if (agentDTO is null)
                {
                    _logger.LogError("Agent object sent from client is null.");
                    return BadRequest("Agent object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Agent object sent from client.");
                    return BadRequest("Invalid model object");
                }
                User user = new User();
                {
                    user.UserId = Guid.NewGuid();
                    user.UserName = agentDTO.Email;
                    user.Password = UserController.EncodePasswordToBase64(agentDTO.Password);
                    user.CreatedDate = DateTime.UtcNow;
                    user.UpdatedDate = DateTime.UtcNow;
                    user.Status = CommonEnums.UserStatus.Active.ToString();
                    user.Role = CommonEnums.UserTypes.Agent.ToString();
                }
                _repository.User.Create(user);
                _repository.Save();

                Agent agent = new Agent();
                agent.AgentId = Guid.NewGuid();
                agent.UserId = user.UserId;
                agent.Name=agentDTO.Name;
                agent.Email=agentDTO.Email;
                agent.Phone = agentDTO.Phone;
                agent.Status = CommonEnums.UserStatus.Active.ToString();
                agent.CreatedDate = DateTime.UtcNow;
                agent.UpdatedDate = DateTime.UtcNow;

                _repository.Agent.CreateAgent(agent);
                _repository.Save();
                return StatusCode(200, "Account created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [Authorize(Roles = "Agent")]
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
        [Authorize(Roles = "Admin")]
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
