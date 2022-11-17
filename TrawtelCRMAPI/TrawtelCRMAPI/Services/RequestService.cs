using Entities.DataTransferObjects;
using Entities.Models;

namespace TrawtelCRMAPI.Services
{
    public class RequestService
    {
        public FlightRequest getFlightRequestDetails(FlightRequest commonFlightRequest)
        {
            if (commonFlightRequest == null)
            {
                commonFlightRequest.ErrorStatus = true;
                commonFlightRequest.ErrorMessage = "Please check the payload";
            }
            else if (commonFlightRequest.flightJourneyRequest == null)
            {
                commonFlightRequest.ErrorStatus = true;
                commonFlightRequest.ErrorMessage = "Please check the payload";
            }
            else if (commonFlightRequest.Adults == 0)
            {
                commonFlightRequest.ErrorStatus = true;
                commonFlightRequest.ErrorMessage = "Please enter the adults";
            }
            return commonFlightRequest;
        }
    }
}
