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
    public class SupplierController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public SupplierController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllSuppliers()
        {
            try
            {
                var Suppliers = _repository.Supplier.GetAllSuppliers();
                var SuppliersResult = _mapper.Map<IEnumerable<SupplierDTO>>(Suppliers);
                return Ok(SuppliersResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllSuppliers action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{SupplierId}")]
        public IActionResult GetSupplierById(Guid SupplierId)
        {
            try
            {
                var owner = _repository.Supplier.GetSupplierById(SupplierId);
                if (owner is null)
                {
                    _logger.LogError($"Supplier with id: {SupplierId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Supplier with id: {SupplierId}");
                    var ownerResult = _mapper.Map<SupplierDTO>(owner);
                    return Ok(ownerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetSupplierById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult CreateSupplier([FromBody] Supplier Supplier)
        {
            try
            {
                if (Supplier is null)
                {
                    _logger.LogError("Supplier object sent from client is null.");
                    return BadRequest("Supplier object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Supplier object sent from client.");
                    return BadRequest("Invalid model object");
                }
                Supplier.SupplierId = Guid.NewGuid();
                Supplier.CreatedDate = DateTime.UtcNow;
                Supplier.UpdatedDate = DateTime.UtcNow;
                var SupplierEntity = _mapper.Map<Supplier>(Supplier);
                _repository.Supplier.CreateSupplier(SupplierEntity);
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
        public IActionResult UpdateSupplier(Guid id, [FromBody] Supplier Supplier)
        {
            try
            {
                if (Supplier is null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var SupplierEntity = _repository.Supplier.GetSupplierById(id);
                if (SupplierEntity is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                Supplier.UpdatedDate = DateTime.UtcNow;
                Supplier.CreatedDate = SupplierEntity.CreatedDate;
                _repository.Supplier.UpdateSupplier(Supplier);
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
        public IActionResult DeleteSupplier(Guid id)
        {
            try
            {
                var Supplier = _repository.Supplier.GetSupplierById(id);
                if (Supplier == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Supplier.DeleteSupplier(Supplier);
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

