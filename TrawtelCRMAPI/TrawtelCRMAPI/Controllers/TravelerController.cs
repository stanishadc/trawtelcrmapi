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
    public class TravelerController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public TravelerController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllTravelers()
        {
            try
            {
                var Clients = _repository.Traveler.GetAllTravelers();
                //var ClientsResult = _mapper.Map<IEnumerable<TravelerDTO>>(Clients);
                return Ok(Clients);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllClients action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{ClientId}")]
        public IActionResult GetTravelerById(Guid TravelerId)
        {
            try
            {
                var owner = _repository.Traveler.GetTravelerById(TravelerId);
                if (owner is null)
                {
                    _logger.LogError($"Client with id: {TravelerId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Client with id: {TravelerId}");
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
        public IActionResult CreateTraveler([FromBody] Traveler traveler)
        {
            try
            {
                if (traveler is null)
                {
                    _logger.LogError("Client object sent from client is null.");
                    return BadRequest("Client object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Client object sent from client.");
                    return BadRequest("Invalid model object");
                }
                traveler.TravelerId = Guid.NewGuid();
                if (traveler.DateOfBirth == null)
                {
                    traveler.DateOfBirth = DateTime.MinValue;
                }
                if (traveler.PassportIssueDate == null)
                {
                    traveler.PassportIssueDate = DateTime.MinValue;
                }
                if (traveler.PassportExpireDate == null)
                {
                    traveler.PassportExpireDate = DateTime.MinValue;
                }
                traveler.CreatedDate = DateTime.UtcNow;
                traveler.UpdatedDate = DateTime.UtcNow;
                var ClientEntity = _mapper.Map<Traveler>(traveler);
                _repository.Traveler.CreateTraveler(ClientEntity);
                _repository.Save();
                return Ok(traveler.TravelerId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTraveler(Guid id, [FromBody] Traveler traveler)
        {
            try
            {
                if (traveler is null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var ClientEntity = _repository.Traveler.GetTravelerById(id);
                if (ClientEntity is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                traveler.UpdatedDate = DateTime.UtcNow;
                traveler.CreatedDate = ClientEntity.CreatedDate;
                _mapper.Map(traveler, ClientEntity);
                _repository.Traveler.UpdateTraveler(ClientEntity);
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
                var Client = _repository.Traveler.GetTravelerById(id);
                if (Client == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Traveler.DeleteTraveler(Client);
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
