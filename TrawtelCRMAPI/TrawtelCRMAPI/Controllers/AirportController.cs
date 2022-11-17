using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace TrawtelCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        public AirportController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        [HttpGet("get")]
        public APIResponse GetAirports()
        {
            APIResponse _apiResponse = new APIResponse();
            try
            {
                var response = _repository.Airport.GetAirports();
                if (response == null)
                {
                    _apiResponse.Status = false;
                    _apiResponse.ErrorMessage = "No Records Found";
                }
                else
                {
                    _apiResponse.Data = response;
                    _apiResponse.Status = true;
                }
            }
            catch (Exception ex)
            {
                _apiResponse.Status = false;
                _apiResponse.ErrorMessage = ex.ToString();
            }
            return _apiResponse;
        }
        [HttpGet("search")]
        public APIResponse SearchAirports(string searchkey)
        {
            APIResponse _apiResponse = new APIResponse();
            try
            {
                var response = _repository.Airport.SearchAirports(searchkey);
                if (response == null)
                {
                    _apiResponse.Status = false;
                    _apiResponse.ErrorMessage = "No Records Found";
                }
                else
                {
                    _apiResponse.Data = response;
                    _apiResponse.Status = true;
                }
            }
            catch (Exception ex)
            {
                _apiResponse.Status = false;
                _apiResponse.ErrorMessage = ex.ToString();
            }
            return _apiResponse;
        }
    }
}
