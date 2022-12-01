using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace TrawtelCRMAPI.Controllers
{
    [Authorize(Roles = "Agent")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public ClientController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllClients()
        {
            try
            {
                var Clients = _repository.Client.GetAllClients();
                var ClientsResult = _mapper.Map<IEnumerable<ClientDTO>>(Clients);
                return Ok(ClientsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllClients action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{ClientId}")]
        public IActionResult GetClientById(Guid ClientId)
        {
            try
            {
                var owner = _repository.Client.GetClientById(ClientId);
                if (owner is null)
                {
                    _logger.LogError($"Client with id: {ClientId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Client with id: {ClientId}");
                    var ownerResult = _mapper.Map<ClientDTO>(owner);
                    return Ok(ownerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetClientById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult CreateClient([FromBody] Client Client)
        {
            try
            {
                if (Client is null)
                {
                    _logger.LogError("Client object sent from client is null.");
                    return BadRequest("Client object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Client object sent from client.");
                    return BadRequest("Invalid model object");
                }
                Client.ClientId = Guid.NewGuid();
                Client.CreatedDate = DateTime.UtcNow;
                Client.UpdatedDate = DateTime.UtcNow;
                var ClientEntity = _mapper.Map<Client>(Client);
                _repository.Client.CreateClient(ClientEntity);
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
        public IActionResult UpdateClient(Guid id, [FromBody] Client Client)
        {
            try
            {
                if (Client is null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var ClientEntity = _repository.Client.GetClientById(id);
                if (ClientEntity is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                Client.UpdatedDate = DateTime.UtcNow;
                Client.CreatedDate = ClientEntity.CreatedDate;
                _mapper.Map(Client, ClientEntity);
                _repository.Client.UpdateClient(ClientEntity);
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
        public IActionResult DeleteClient(Guid id)
        {
            try
            {
                var Client = _repository.Client.GetClientById(id);
                if (Client == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Client.DeleteClient(Client);
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

