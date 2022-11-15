using Entities.DataTransferObjects;
using Entities.Models;

namespace TrawtelCRMAPI.Services
{
    public class RequestService
    {
        public FlightRequestDetails getFlightRequestDetails(RequestDTO flightRequest)
        {
            FlightRequestDetails flightRequestDetails = new FlightRequestDetails();
            if (flightRequest == null)
            {
                flightRequestDetails.Error = "Please check the payload";
            }
            else if (flightRequest.TravelRequest == null)
            {
                flightRequestDetails.Error = "Please check the payload";
            }
            else if (flightRequest.TravelRequest.FlightRequestDetails == null)
            {
                flightRequestDetails.Error = "Please check the payload";
            }
            else if (flightRequest.TravelRequest.FlightRequestDetails.NoOfAdults == 0)
            {
                flightRequestDetails.Error= "Please enter the adults";
            }
            else
            {
                flightRequestDetails = flightRequest.TravelRequest.FlightRequestDetails;
            }
            return flightRequestDetails;
        }
    }
}
