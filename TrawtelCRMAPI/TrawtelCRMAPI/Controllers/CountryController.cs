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
    public class CountryController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        public CountryController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        [HttpGet("get")]
        public Response<List<Country>> GetCountries()
        {
            Response<List<Country>> _apiResponse = new Response<List<Country>>();
            try
            {
                var response = _repository.Country.GetCountries();
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
        public Response<List<Country>> SearchCountries(string searchkey)
        {
            Response<List<Country>> _apiResponse = new Response<List<Country>>();
            try
            {
                var response = _repository.Country.SearchCountry(searchkey);
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
