using Amazon.DynamoDBv2.DataModel;
using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrawtelCRMAPI.DynamoDB;
using TrawtelCRMAPI.Services;

namespace TrawtelCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        RequestService requestService = new RequestService();
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        FlightService _flightService;
        private readonly IDynamoDBContext _context;
        public FlightController(ILoggerManager logger, IRepositoryWrapper repository, IAmazonS3 s3Client, IDynamoDBContext context)
        {
            _logger = logger;
            _repository = repository;
            _flightService = new FlightService(s3Client);
            _context = context;
        }
        [Route("Search")]
        [HttpPost]
        public APIResponse SearcAllFlights(RequestDTO request)
        {
            APIResponse _objResponse = new APIResponse();
            try
            {
                var flightRequestDetails = requestService.getFlightRequestDetails(request);
                if (string.IsNullOrEmpty(flightRequestDetails.Error))
                {
                    var supplierdetails = _repository.SupplierCode.GetDefaultSupplierByAgentId(request.AgentId, request.TravelType);
                    if (supplierdetails.Count > 0)
                    {
                        var flightsResponse = _repository.Flight.SearchFlights(flightRequestDetails, request.AgentId, supplierdetails);
                        if ((bool)flightsResponse.status)
                        {
                            var flightsData = flightsResponse.commonFlightDetails;
                            if (flightsData?.Count > 0)
                            {
                                flightsData = _flightService.CustomizeFlights(flightsData);

                                _objResponse.Data = flightsData;
                                _objResponse.Status = true;
                                return _objResponse;
                            }
                            else
                            {
                                _objResponse.ErrorMessage = "No Flights Found";
                                _objResponse.Status = false;
                                return _objResponse;
                            }
                        }
                        else
                        {
                            _objResponse.ErrorMessage = flightsResponse.ErrorMessage;
                            _objResponse.Status = false;
                            return _objResponse;
                        }
                    }
                    else
                    {
                        _objResponse.ErrorMessage = "No Active Supplier";
                        _objResponse.Status = false;
                        return _objResponse;
                    }
                }
                else
                {
                    _objResponse.ErrorMessage = flightRequestDetails.Error;
                    _objResponse.Status = false;
                    return _objResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception occurred in the Flight Search Request : " + ex);
                _objResponse.ErrorMessage = "Error in Search Request";
                _objResponse.Status = false;
                return _objResponse;
            }
        }
        [Route("Travelers")]
        [HttpPost]
        public APIResponse FlightTravellers(RequestDTO request)
        {
            APIResponse _objResponse = new APIResponse();
            try
            {
                var flightRequestDetails = requestService.getFlightRequestDetails(request);
                if (string.IsNullOrEmpty(flightRequestDetails.Error))
                {
                    var supplierdetails = _repository.SupplierCode.GetDefaultSupplierByAgentId(request.AgentId, request.TravelType);
                    if (supplierdetails.Count > 0)
                    {
                        var flightsResponse = _repository.Flight.SearchFlights(flightRequestDetails, request.AgentId, supplierdetails);
                        if ((bool)flightsResponse.status)
                        {
                            var flightsData = flightsResponse.commonFlightDetails;
                            if (flightsData?.Count > 0)
                            {

                                flightsData = _flightService.CustomizeFlights(flightsData);

                                _objResponse.Data = flightsData;
                                _objResponse.Status = true;
                                return _objResponse;
                            }
                            else
                            {
                                _objResponse.ErrorMessage = "No Flights Found";
                                _objResponse.Status = false;
                                return _objResponse;
                            }
                        }
                        else
                        {
                            _objResponse.ErrorMessage = flightsResponse.ErrorMessage;
                            _objResponse.Status = false;
                            return _objResponse;
                        }
                    }
                    else
                    {
                        _objResponse.ErrorMessage = "No Active Supplier";
                        _objResponse.Status = false;
                        return _objResponse;
                    }
                }
                else
                {
                    _objResponse.ErrorMessage = flightRequestDetails.Error;
                    _objResponse.Status = false;
                    return _objResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception occurred in the Flight Search Request : " + ex);
                _objResponse.ErrorMessage = "Error in Search Request";
                _objResponse.Status = false;
                return _objResponse;
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent(searchdata searchdatas)
        {
            DateTime currentTime = DateTime.Now;
            DateTime x30MinsLater = currentTime.AddMinutes(5);
            searchdatas.ttl = x30MinsLater;
            await _context.SaveAsync(searchdatas);
            return Ok(searchdatas);
        }
    }
}
