using Amazon.DynamoDBv2.DataModel;
using Amazon.S3;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TrawtelCRMAPI.Services;

namespace TrawtelCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        HotelService _HotelService;
        private readonly IDynamoDBContext _context;
        public HotelController(ILoggerManager logger, IRepositoryWrapper repository, IAmazonS3 s3Client, IDynamoDBContext context)
        {
            _logger = logger;
            _repository = repository;
            _HotelService = new HotelService(s3Client);
            _context = context;
        }      
        [HttpPost]
        public IActionResult CreateHotelRequest([FromBody] HotelRequestDTO commonHotelRequest)
        {
            try
            {
                commonHotelRequest = _HotelService.ValidateHotelRequest(commonHotelRequest);
                if (commonHotelRequest.ErrorStatus)
                {
                    _logger.LogError($"Something went wrong inside CreateOwner action: {commonHotelRequest.ErrorMessage}");
                    return StatusCode(500, commonHotelRequest.ErrorMessage);
                }
                HotelRequest HotelRequest = new HotelRequest();
                HotelRequest.HotelRequestId = Guid.NewGuid();
                HotelRequest.AgentId = commonHotelRequest.AgentId;
                HotelRequest.ClientId = commonHotelRequest.ClientId;
                HotelRequest.CheckIn = commonHotelRequest.CheckIn;
                HotelRequest.CheckOut = commonHotelRequest.CheckOut;
                HotelRequest.Status = CommonEnums.Status.New.ToString();
                HotelRequest.CreatedDate = DateTime.UtcNow;
                HotelRequest.UpdatedDate = DateTime.UtcNow;
                HotelRequest.RoomDetails = JsonConvert.SerializeObject(new { commonHotelRequest.RoomDetails });
                HotelRequest.Location = JsonConvert.SerializeObject(new { commonHotelRequest.Location });

                _repository.Hotel.CreateHotelRequest(HotelRequest);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, commonHotelRequest.ErrorMessage);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateHotelRequest(Guid id, [FromBody] HotelRequestDTO commonHotelRequest)
        {
            try
            {
                commonHotelRequest = _HotelService.ValidateHotelRequest(commonHotelRequest);
                if (commonHotelRequest.ErrorStatus)
                {
                    _logger.LogError($"Something went wrong inside CreateOwner action: {commonHotelRequest.ErrorMessage}");
                    return StatusCode(500, commonHotelRequest.ErrorMessage);
                }
                var HotelEntity = _repository.Hotel.GetHotelRequestById(id);
                if (HotelEntity is null)
                {
                    _logger.LogError($"Hotel with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                HotelRequest HotelRequest = new HotelRequest();
                HotelRequest.HotelRequestId = Guid.NewGuid();
                HotelRequest.AgentId = commonHotelRequest.AgentId;
                HotelRequest.ClientId = commonHotelRequest.ClientId;
                HotelRequest.CheckIn = commonHotelRequest.CheckIn;
                HotelRequest.CheckOut = commonHotelRequest.CheckOut;
                HotelRequest.Status = CommonEnums.Status.New.ToString();
                HotelRequest.CreatedDate = DateTime.UtcNow;
                HotelRequest.UpdatedDate = DateTime.UtcNow;
                HotelRequest.RoomDetails = JsonConvert.SerializeObject(new { commonHotelRequest.RoomDetails });
                HotelRequest.Location = JsonConvert.SerializeObject(new { commonHotelRequest.Location });

                _repository.Hotel.UpdateHotelRequest(HotelRequest);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, commonHotelRequest.ErrorMessage);
            }
        }

        [HttpGet("GetHotelRequestById/{RequestId}")]
        public IActionResult GetHotelRequestById(Guid RequestId)
        {
            try
            {
                var HotelRequest = _repository.Hotel.GetHotelRequestById(RequestId);
                if (HotelRequest is null)
                {
                    _logger.LogError($"Hotel Request with id: {RequestId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Hotel with id: {RequestId}");
                    var ownerResult = getRequestDTO(HotelRequest);
                    return Ok(ownerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetHotelRequestById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetHotelRequestsByAgent/{AgentId}")]
        public IActionResult GetHotelRequestsByAgent(Guid AgentId)
        {
            try
            {
                var HotelRequest = _repository.Hotel.GetHotelRequestsByAgent(AgentId);
                List<HotelRequestDTO> listRequests = new List<HotelRequestDTO>();
                if (HotelRequest is null)
                {
                    _logger.LogError($"Hotel Request with id: {AgentId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Agent with id: {AgentId}");
                    foreach (var request in HotelRequest)
                    {
                        listRequests.Add(getRequestDTO(request));
                    }
                    return Ok(listRequests);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAgentById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetHotelRequestsByClient/{ClientId}")]
        public IActionResult GetHotelRequestsByClient(Guid ClientId)
        {
            try
            {
                var HotelRequest = _repository.Hotel.GetHotelRequestsByClient(ClientId);
                List<HotelRequestDTO> listRequests = new List<HotelRequestDTO>();
                if (HotelRequest is null)
                {
                    _logger.LogError($"Hotel Request with id: {ClientId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Agent with id: {ClientId}");
                    foreach (var request in HotelRequest)
                    {
                        listRequests.Add(getRequestDTO(request));
                    }
                    return Ok(listRequests);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAgentById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        private HotelRequestDTO getRequestDTO(HotelRequest HotelRequest)
        {
            HotelRequestDTO HotelRequestDTO = new HotelRequestDTO();
            HotelRequestDTO.HotelRequestId = HotelRequest.HotelRequestId;
            HotelRequestDTO.AgentId = HotelRequest.AgentId;
            HotelRequestDTO.ClientId = HotelRequest.ClientId;
            HotelRequestDTO.CheckIn = HotelRequest.CheckIn;
            HotelRequestDTO.CheckOut = HotelRequest.CheckOut;
            HotelRequestDTO.Status = HotelRequest.Status;
            HotelRequestDTO.CreatedDate = HotelRequest.CreatedDate;
            HotelRequestDTO.UpdatedDate = HotelRequest.UpdatedDate;

            var roommodel = JsonConvert.DeserializeObject<HotelRequestRoot>(HotelRequest.RoomDetails);
            HotelRequestDTO.RoomDetails = roommodel?.roomDetails;

            var locationmodel = JsonConvert.DeserializeObject<HotelRequestRoot>(HotelRequest.Location);
            HotelRequestDTO.Location = locationmodel?.location;

            return HotelRequestDTO;
        }
    }
}
