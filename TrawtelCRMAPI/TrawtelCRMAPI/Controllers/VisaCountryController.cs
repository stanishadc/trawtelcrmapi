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
    public class VisaCountryController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public VisaCountryController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllVisaCountrys()
        {
            try
            {
                var VisaCountrys = _repository.VisaCountry.GetAllVisaCountries();
                var VisaCountrysResult = _mapper.Map<IEnumerable<VisaCountryDTO>>(VisaCountrys);
                return Ok(VisaCountrysResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllVisaCountrys action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{VisaCountryId}")]
        public IActionResult GetVisaCountryById(Guid VisaCountryId)
        {
            try
            {
                var owner = _repository.VisaCountry.GetVisaCountryById(VisaCountryId);
                if (owner is null)
                {
                    _logger.LogError($"VisaCountry with id: {VisaCountryId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned VisaCountry with id: {VisaCountryId}");
                    var ownerResult = _mapper.Map<VisaCountryDTO>(owner);
                    return Ok(ownerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVisaCountryById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult CreateVisaCountry([FromBody] VisaCountry VisaCountry)
        {
            try
            {
                if (VisaCountry is null)
                {
                    _logger.LogError("VisaCountry object sent from client is null.");
                    return BadRequest("VisaCountry object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid VisaCountry object sent from client.");
                    return BadRequest("Invalid model object");
                }
                VisaCountry.VisaCountryId = Guid.NewGuid();
                var VisaCountryEntity = _mapper.Map<VisaCountry>(VisaCountry);
                _repository.VisaCountry.CreateVisaCountry(VisaCountryEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateVisaCountry(Guid id, [FromBody] VisaCountry VisaCountry)
        {
            try
            {
                if (VisaCountry is null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var VisaCountryEntity = _repository.VisaCountry.GetVisaCountryById(id);
                if (VisaCountryEntity is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(VisaCountry, VisaCountryEntity);
                _repository.VisaCountry.UpdateVisaCountry(VisaCountryEntity);
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
        public IActionResult DeleteVisaCountry(Guid id)
        {
            try
            {
                var VisaCountry = _repository.VisaCountry.GetVisaCountryById(id);
                if (VisaCountry == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.VisaCountry.DeleteVisaCountry(VisaCountry);
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
