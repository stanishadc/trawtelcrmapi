using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrawtelCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        public CityController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        [HttpGet("get")]
        public APIResponse GetCities()
        {
            APIResponse _apiResponse = new APIResponse();
            try
            {
                var response = _repository.City.GetCities();
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
        public APIResponse SearchCities(string searchkey)
        {
            APIResponse _apiResponse = new APIResponse();
            try
            {
                var response = _repository.City.SearchCity(searchkey);
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
