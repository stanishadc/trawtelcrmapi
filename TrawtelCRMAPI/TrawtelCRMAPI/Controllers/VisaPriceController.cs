using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrawtelCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisaPriceController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public VisaPriceController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllVisaPrices()
        {
            try
            {
                var VisaPrices = _repository.VisaPrice.GetAllVisaPrices();
                var VisaPricesResult = _mapper.Map<IEnumerable<VisaPriceDTO>>(VisaPrices);
                return Ok(VisaPricesResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllVisaPrices action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{VisaPriceId}")]
        public IActionResult GetVisaPriceById(Guid VisaPriceId)
        {
            try
            {
                var owner = _repository.VisaPrice.GetVisaPriceById(VisaPriceId);
                if (owner is null)
                {
                    _logger.LogError($"VisaPrice with id: {VisaPriceId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned VisaPrice with id: {VisaPriceId}");
                    var ownerResult = _mapper.Map<VisaPriceDTO>(owner);
                    return Ok(ownerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVisaPriceById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult CreateVisaPrice([FromBody] VisaPrice VisaPrice)
        {
            try
            {
                if (VisaPrice is null)
                {
                    _logger.LogError("VisaPrice object sent from client is null.");
                    return BadRequest("VisaPrice object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid VisaPrice object sent from client.");
                    return BadRequest("Invalid model object");
                }
                VisaPrice.VisaPriceId = Guid.NewGuid();
                var VisaPriceEntity = _mapper.Map<VisaPrice>(VisaPrice);
                _repository.VisaPrice.CreateVisaPrice(VisaPriceEntity);
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
        public IActionResult UpdateVisaPrice(Guid id, [FromBody] VisaPrice VisaPrice)
        {
            try
            {
                if (VisaPrice is null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var VisaPriceEntity = _repository.VisaPrice.GetVisaPriceById(id);
                if (VisaPriceEntity is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(VisaPrice, VisaPriceEntity);
                _repository.VisaPrice.UpdateVisaPrice(VisaPriceEntity);
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
        public IActionResult DeleteVisaPrice(Guid id)
        {
            try
            {
                var VisaPrice = _repository.VisaPrice.GetVisaPriceById(id);
                if (VisaPrice == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.VisaPrice.DeleteVisaPrice(VisaPrice);
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

