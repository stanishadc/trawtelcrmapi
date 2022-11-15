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
    public class SupplierCodeController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public SupplierCodeController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllClientCodes()
        {
            try
            {
                var ClientCodes = _repository.SupplierCode.GetAllSupplierCodes();
                var ClientCodesResult = _mapper.Map<IEnumerable<SupplierCodeDTO>>(ClientCodes);
                return Ok(ClientCodesResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllClientCodes action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{ClientCodeId}")]
        public IActionResult GetClientCodeById(Guid ClientCodeId)
        {
            try
            {
                var owner = _repository.SupplierCode.GetSupplierCodeById(ClientCodeId);
                if (owner is null)
                {
                    _logger.LogError($"ClientCode with id: {ClientCodeId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned ClientCode with id: {ClientCodeId}");
                    var ownerResult = _mapper.Map<SupplierCodeDTO>(owner);
                    return Ok(ownerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetClientCodeById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult CreateClientCode([FromBody] SupplierCode supplierCode)
        {
            try
            {
                if (supplierCode is null)
                {
                    _logger.LogError("ClientCode object sent from client is null.");
                    return BadRequest("ClientCode object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid ClientCode object sent from client.");
                    return BadRequest("Invalid model object");
                }
                supplierCode.SupplierCodeId = Guid.NewGuid();
                supplierCode.CreatedDate = DateTime.UtcNow;
                supplierCode.UpdatedDate = DateTime.UtcNow;
                var ClientCodeEntity = _mapper.Map<SupplierCode>(supplierCode);
                _repository.SupplierCode.CreateSupplierCode(ClientCodeEntity);
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
        public IActionResult UpdateClientCode(Guid id, [FromBody] SupplierCode supplierCode)
        {
            try
            {
                if (supplierCode is null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var ClientCodeEntity = _repository.SupplierCode.GetSupplierCodeById(id);
                if (ClientCodeEntity is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                supplierCode.UpdatedDate = DateTime.UtcNow;
                supplierCode.CreatedDate = ClientCodeEntity.CreatedDate;
                _mapper.Map(supplierCode, ClientCodeEntity);
                _repository.SupplierCode.UpdateSupplierCode(ClientCodeEntity);
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
        public IActionResult DeleteClientCode(Guid id)
        {
            try
            {
                var ClientCode = _repository.SupplierCode.GetSupplierCodeById(id);
                if (ClientCode == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.SupplierCode.DeleteSupplierCode(ClientCode);
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
