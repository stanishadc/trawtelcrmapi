using Contracts;
using Entities.Common;
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
    public class CityController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        public CityController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        [HttpGet("get")]
        public Response<List<City>> GetCities()
        {
            Response<List<City>> _apiResponse = new Response<List<City>>();
            try
            {
                var response = _repository.City.GetCities();
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
        public Response<List<City>> SearchCities(string searchkey)
        {
            Response<List<City>> _apiResponse = new Response<List<City>>();
            try
            {
                var response = _repository.City.SearchCity(searchkey);
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
