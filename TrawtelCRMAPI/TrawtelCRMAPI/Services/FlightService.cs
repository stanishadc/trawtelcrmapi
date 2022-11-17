using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime.Internal;
using Amazon.S3;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TrawtelCRMAPI.Services
{
    public class FlightService
    {
        private readonly AmazonService _amazonService;
        public FlightService(IAmazonS3 s3Client)
        {
            _amazonService = new AmazonService(s3Client);
        }
        public FlightRequestDTO ValidateFlightRequest(FlightRequestDTO flightRequestDTO)
        {
            if (flightRequestDTO == null)
            {
                flightRequestDTO.ErrorStatus = true;
                flightRequestDTO.ErrorMessage = "Please check the payload";
            }
            else if (flightRequestDTO.flightJourneyRequest == null)
            {
                flightRequestDTO.ErrorStatus = true;
                flightRequestDTO.ErrorMessage = "Please check the payload";
            }
            else if (flightRequestDTO.Adults == 0)
            {
                flightRequestDTO.ErrorStatus = true;
                flightRequestDTO.ErrorMessage = "Please enter the adults";
            }
            return flightRequestDTO;
        }
        public FlightRequestDTO GetFlightRequestDetails(FlightRequestDTO flightRequestDTO)
        {
            flightRequestDTO = ValidateFlightRequest(flightRequestDTO);
            if (flightRequestDTO.ErrorStatus)
            {
                return flightRequestDTO;
            }
            
            return flightRequestDTO;
        }
        public List<CommonFlightDetails> CustomizeFlights(List<CommonFlightDetails> commonFlightDetailsList)
        {
            //commonFlightDetailsList = getAirlineLogos(commonFlightDetailsList);
            commonFlightDetailsList = FilterFlights(commonFlightDetailsList);
            commonFlightDetailsList = SortFlights(commonFlightDetailsList);
            return commonFlightDetailsList;
        }

        private List<CommonFlightDetails> getAirlineLogos(List<CommonFlightDetails> commonFlightDetailsList)
        {
            for (int i = 0; i < commonFlightDetailsList.Count; i++)
            {
                for (int j = 0; j < commonFlightDetailsList[i].tFLegs.Count; j++)
                {
                    for(int k = 0; k < commonFlightDetailsList[i].tFLegs[j].tFSegments.Count; k++)
                    {
                        commonFlightDetailsList[i].tFLegs[j].tFSegments[k].AirlineLogo = _amazonService.getAirportlogo(commonFlightDetailsList[i].tFLegs[j].tFSegments[k].AirlineCode);
                    }
                }
            }
            return commonFlightDetailsList;
        }
        public List<CommonFlightDetails> FilterFlights(List<CommonFlightDetails> commonFlightDetailsList)
        {
            return commonFlightDetailsList;
        }
        public List<CommonFlightDetails> SortFlights(List<CommonFlightDetails> commonFlightDetailsList)
        {
            return commonFlightDetailsList;
        }
    }
}
