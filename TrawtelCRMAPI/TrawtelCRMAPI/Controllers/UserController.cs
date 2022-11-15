using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace TrawtelCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public UserController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _repository.User.GetAllUsers();
                var usersResult = _mapper.Map<IEnumerable<UserDTO>>(users);
                return Ok(usersResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllUsers action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{UserId}")]
        public IActionResult GetUserById(Guid UserId)
        {
            try
            {
                var owner = _repository.User.GetUserById(UserId);
                if (owner is null)
                {
                    _logger.LogError($"User with id: {UserId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned User with id: {UserId}");
                    var ownerResult = _mapper.Map<UserDTO>(owner);
                    return Ok(ownerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                if (user is null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return BadRequest("User object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid User object sent from client.");
                    return BadRequest("Invalid model object");
                }
                user.UserId = Guid.NewGuid();
                user.Password = EncodePasswordToBase64(user.Password);
                user.CreatedDate = DateTime.UtcNow;
                user.UpdatedDate = DateTime.UtcNow;
                var userEntity = _mapper.Map<User>(user);
                _repository.User.CreateUser(userEntity);
                _repository.Save();

                UserKey userKey = new UserKey();
                {
                    userKey.UserKeyId = Guid.NewGuid();
                    userKey.AgentId = user.AgentId;
                    userKey.SecretKey = GenerateAPIKEY();
                    userKey.IPAddress = "";
                    userKey.Status = "0";
                    userKey.CreatedDate = DateTime.Now;
                    userKey.UpdatedDate = DateTime.Now;

                }
                _repository.UserKey.CreateUserKey(userKey);
                _repository.Save();
                return Ok("User created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody] User user)
        {
            try
            {
                if (user is null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }
                var userEntity = _repository.User.GetUserById(id);
                if (userEntity is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                user.UpdatedDate = DateTime.UtcNow;
                user.CreatedDate = userEntity.CreatedDate;
                _mapper.Map(user, userEntity);
                _repository.User.UpdateUser(userEntity);
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
        public IActionResult DeleteUser(Guid id)
        {
            try
            {
                var user = _repository.User.GetUserById(id);
                if (user == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.User.DeleteUser(user);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        public static string GenerateAPIKEY()
        {
            var key = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            return Convert.ToBase64String(key);
        }
        //this function Convert to Encord your Password
        public static string EncodePasswordToBase64(string? password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        //this function Convert to Decord your Password
        public static string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
    }
}
