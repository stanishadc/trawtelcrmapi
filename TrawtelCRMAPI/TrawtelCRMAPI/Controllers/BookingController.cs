using AutoMapper;
using Contracts;
using Entities;
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
    public class BookingController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public BookingController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllBookings()
        {
            try
            {
                var Bookings = _repository.Booking.GetAllBookings();
                var BookingResult = _mapper.Map<IEnumerable<Booking>>(Bookings);
                return Ok(BookingResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Bookings action: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{BookingId}")]
        public IActionResult GetBookingById(Guid BookingId)
        {
            try
            {
                var owner = _repository.Booking.GetBookingById(BookingId);
                if (owner is null)
                {
                    _logger.LogError($"Booking with id: {BookingId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned Booking with id: {BookingId}");
                    var ownerResult = _mapper.Map<Booking>(owner);
                    return Ok(ownerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAgentById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        //[HttpPost]
        //public IActionResult GetBookingByClientId(Guid ClientId)
        //{
        //    try
        //    {
        //        var owner = _repository.Booking.GetBookingByClientId(ClientId);
        //        if (owner is null)
        //        {
        //            _logger.LogError($"Booking with id: {ClientId}, hasn't been found in db.");
        //            return NotFound();
        //        }
        //        else
        //        {
        //            _logger.LogInfo($"Returned Booking with id: {ClientId}");
        //            var ownerResult = _mapper.Map<Booking>(owner);
        //            return Ok(ownerResult);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong inside GetAgentById action: {ex.Message}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}
        //[HttpPost]
        //public IActionResult GetBookingByTravelType(char TravelType)
        //{
        //    try
        //    {
        //        var owner = _repository.Booking.GetBookingByTravelType(TravelType);
        //        if (owner is null)
        //        {
        //            _logger.LogError($"Booking with id: {TravelType}, hasn't been found in db.");
        //            return NotFound();
        //        }
        //        else
        //        {
        //            _logger.LogInfo($"Returned Booking with id: {TravelType}");
        //            var ownerResult = _mapper.Map<Booking>(owner);
        //            return Ok(ownerResult);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong inside GetAgentById action: {ex.Message}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}
        [HttpPost]
        public IActionResult CreateBooking([FromBody] Booking booking)
        {
            try
            {
                if (booking is null)
                {
                    _logger.LogError("Agent object sent from client is null.");
                    return BadRequest("Agent object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Agent object sent from client.");
                    return BadRequest("Invalid model object");
                }
                booking.BookingId = Guid.NewGuid();
                booking.BookingDate = DateTime.UtcNow;
                var bookingEntity = _mapper.Map<Booking>(booking);
                _repository.Booking.CreateBooking(bookingEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(Guid id)
        {
            try
            {
                var Booking = _repository.Booking.GetBookingById(id);
                if (Booking == null)
                {
                    _logger.LogError($"Booking with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repository.Booking.DeleteBooking(Booking);
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

