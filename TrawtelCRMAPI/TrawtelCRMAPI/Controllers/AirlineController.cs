using Contracts;
using Entities.Common;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrawtelCRMAPI.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        public AirlineController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        [HttpGet("get")]
        public Response<List<Airline>> GetAirports()
        {
            Response<List<Airline>> _apiResponse = new Response<List<Airline>>();
            try
            {
                var response = _repository.Airline.GetAirlines();
                if (response.Count > 0)
                {
                    _apiResponse.Data = response;
                    _apiResponse.Succeeded = true;
                }
                else
                {
                    _apiResponse.Succeeded = false;
                    _apiResponse.Message = "No Records Found";
                }
            }
            catch (Exception ex)
            {
                _apiResponse.Succeeded = false;
                _apiResponse.Message = ex.ToString();
            }
            return _apiResponse;
        }
        [HttpGet("search")]
        public Response<List<Airline>> SearchAirports(string searchkey)
        {
            Response<List<Airline>> _apiResponse = new Response<List<Airline>>();
            try
            {
                var response = _repository.Airline.SearchAirlines(searchkey);
                if (response.Count > 0)
                {
                    _apiResponse.Data = response;
                    _apiResponse.Succeeded = true;
                }
                else
                {
                    _apiResponse.Succeeded = false;
                    _apiResponse.Message = "No Records Found";
                }
            }
            catch (Exception ex)
            {
                _apiResponse.Succeeded = false;
                _apiResponse.Message = ex.ToString();
            }
            return _apiResponse;
        }
    }
}
