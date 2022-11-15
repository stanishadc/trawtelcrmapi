using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft;
using Newtonsoft.Json;

namespace TrawtelCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisaController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public VisaController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        private VisaDTO GetVisaDTO(Visa visa)
        {
            VisaDTO visaDTO = new VisaDTO();
            var vdmodel = JsonConvert.DeserializeObject<VisaRoot>(visa.VisaDocuments);
            var vtmodel = JsonConvert.DeserializeObject<VisaRoot>(visa.VisaTerms);
            visaDTO.Entry = visa.Entry;
            visaDTO.VisaName = visa.VisaName;
            visaDTO.VisaId = visa.VisaId;
            visaDTO.VisaCountryId = visa.VisaCountryId;
            visaDTO.VisaTerms = vtmodel.VisaTerms;
            visaDTO.VisaDocuments = vdmodel.VisaDocuments;
            visaDTO.ProcessingTime = visa.ProcessingTime;
            visaDTO.StayPeriod = visa.StayPeriod;
            visaDTO.Validity = visa.Validity;
            visaDTO.VisaType = visa.VisaType;
            return visaDTO;
        }
        [HttpGet]
        public IActionResult GetAllVisas()
        {
            try
            {
                var Visas = _repository.Visa.GetAllVisas();
                List<VisaDTO> listvisas = new List<VisaDTO>();
                foreach (var visa in Visas)
                {                    
                    listvisas.Add(GetVisaDTO(visa));
                }
                return Ok(listvisas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllVisas action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{VisaId}")]
        public IActionResult GetVisaById(Guid VisaId)
        {
            try
            {
                var visa = _repository.Visa.GetVisaById(VisaId);
                if (visa is null)
                {
                    _logger.LogError($"Visa with id: {VisaId}, hasn't been found in db.");
                    return NotFound();
                }
                
                _logger.LogInfo($"Returned Visa with id: {VisaId}");
                return Ok(GetVisaDTO(visa));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVisaById action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetVisasByCountryName/{VisaCountry}")]
        public IActionResult GetVisasByCountryName(string VisaCountry)
        {
            try
            {
                var Visas = _repository.Visa.GetVisasByCountryName(VisaCountry);
                if (Visas.Count == 0)
                {
                    _logger.LogError($"Visa with id: {VisaCountry}, hasn't been found in db.");
                    return NotFound();
                }
                List<VisaDTO> listvisas = new List<VisaDTO>();
                foreach (var visa in Visas)
                {
                    VisaDTO visaDTO = new VisaDTO();
                    var vdmodel = JsonConvert.DeserializeObject<VisaRoot>(visa.VisaDocuments);
                    var vtmodel = JsonConvert.DeserializeObject<VisaRoot>(visa.VisaTerms);
                    visaDTO.Entry = visa.Entry;
                    visaDTO.VisaName = visa.VisaName;
                    visaDTO.VisaId = visa.VisaId;
                    visaDTO.VisaCountryId = visa.VisaCountryId;
                    visaDTO.VisaTerms = vtmodel.VisaTerms;
                    visaDTO.VisaDocuments = vdmodel.VisaDocuments;
                    visaDTO.ProcessingTime = visa.ProcessingTime;
                    visaDTO.StayPeriod = visa.StayPeriod;
                    visaDTO.Validity = visa.Validity;
                    visaDTO.VisaType = visa.VisaType;
                    listvisas.Add(visaDTO);
                }
                return Ok(listvisas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVisaById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("GetVisasByCountryId/{CountryId}")]
        public IActionResult GetVisasByCountryId(string CountryId)
        {
            try
            {
                var Visas = _repository.Visa.GetVisasByCountryId(CountryId);
                if (Visas.Count == 0)
                {
                    _logger.LogError($"Visa with id: {CountryId}, hasn't been found in db.");
                    return NotFound();
                }
                List<VisaDTO> listvisas = new List<VisaDTO>();
                foreach (var visa in Visas)
                {
                    VisaDTO visaDTO = new VisaDTO();
                    var vdmodel = JsonConvert.DeserializeObject<VisaRoot>(visa.VisaDocuments);
                    var vtmodel = JsonConvert.DeserializeObject<VisaRoot>(visa.VisaTerms);
                    visaDTO.Entry = visa.Entry;
                    visaDTO.VisaName = visa.VisaName;
                    visaDTO.VisaId = visa.VisaId;
                    visaDTO.VisaCountryId = visa.VisaCountryId;
                    visaDTO.VisaTerms = vtmodel.VisaTerms;
                    visaDTO.VisaDocuments = vdmodel.VisaDocuments;
                    visaDTO.ProcessingTime = visa.ProcessingTime;
                    visaDTO.StayPeriod = visa.StayPeriod;
                    visaDTO.Validity = visa.Validity;
                    visaDTO.VisaType = visa.VisaType;
                    listvisas.Add(visaDTO);
                }
                return Ok(listvisas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetVisaById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult CreateVisa([FromBody] VisaDTO visaDTO)
        {
            try
            {
                if (visaDTO is null)
                {
                    _logger.LogError("Visa object sent from client is null.");
                    return BadRequest("Visa object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Visa object sent from client.");
                    return BadRequest("Invalid model object");
                }
                Visa VisaEntity = new Visa();
                VisaEntity.VisaId = Guid.NewGuid();
                VisaEntity.Entry = visaDTO.Entry;
                VisaEntity.VisaName = visaDTO.VisaName;
                VisaEntity.VisaCountryId = visaDTO.VisaCountryId;
                var visaterms = JsonConvert.SerializeObject(new { visaDTO.VisaTerms });
                VisaEntity.VisaTerms = visaterms;
                var visadocuments = JsonConvert.SerializeObject(new { visaDTO.VisaDocuments });
                VisaEntity.VisaDocuments = visadocuments;
                VisaEntity.ProcessingTime = visaDTO.ProcessingTime;
                VisaEntity.StayPeriod = visaDTO.StayPeriod;
                VisaEntity.Validity = visaDTO.Validity;
                VisaEntity.VisaType = visaDTO.VisaType;
                VisaEntity.CreatedDate = DateTime.UtcNow;
                VisaEntity.UpdatedDate = DateTime.UtcNow;
                _repository.Visa.CreateVisa(VisaEntity);
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
        public IActionResult UpdateVisa(Guid id, [FromBody] VisaDTO visaDTO)
        {
            try
            {
                if (visaDTO is null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var VisaEntity = _repository.Visa.GetVisaById(id);
                if (VisaEntity is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                VisaEntity.Entry = visaDTO.Entry;
                VisaEntity.VisaName = visaDTO.VisaName;
                VisaEntity.VisaCountryId = visaDTO.VisaCountryId;
                var visaterms = JsonConvert.SerializeObject(new { VisaTerm = visaDTO.VisaTerms });
                VisaEntity.VisaTerms = visaterms;
                var visadocuments = JsonConvert.SerializeObject(new { VisaDocument = visaDTO.VisaDocuments });
                VisaEntity.VisaDocuments = visadocuments;
                VisaEntity.ProcessingTime = visaDTO.ProcessingTime;
                VisaEntity.StayPeriod = visaDTO.StayPeriod;
                VisaEntity.Validity = visaDTO.Validity;
                VisaEntity.VisaType = visaDTO.VisaType;
                VisaEntity.UpdatedDate = DateTime.UtcNow;
                _repository.Visa.UpdateVisa(VisaEntity);
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
        public IActionResult DeleteVisa(Guid id)
        {
            try
            {
                var Visa = _repository.Visa.GetVisaById(id);
                if (Visa == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Visa.DeleteVisa(Visa);
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
