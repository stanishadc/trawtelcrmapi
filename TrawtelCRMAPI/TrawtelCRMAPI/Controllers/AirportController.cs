using AutoMapper;
using Contracts;
using Entities.Common;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        public Response<List<Airport>> GetAirports()
        {
            Response<List<Airport>> _apiResponse = new Response<List<Airport>>();
            try
            {
                var response = _repository.Airport.GetAirports();
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
        public Response<List<Airport>> SearchAirports(string searchkey)
        {
            Response<List<Airport>> _apiResponse = new Response<List<Airport>>();
            try
            {
                var response = _repository.Airport.SearchAirports(searchkey);
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
