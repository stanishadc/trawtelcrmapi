using Amazon.S3;
using Entities.Models;

namespace TrawtelCRMAPI.Services
{
    public class ValidationService
    {
        public FlightRequestDTO ValidateFlightRequest(FlightRequestDTO flightRequestDTO)
        {
            if (flightRequestDTO == null)
            {
                flightRequestDTO.ErrorStatus = true;
                flightRequestDTO.ErrorMessage = "Please check the payload";
                return flightRequestDTO;
            }
            else if (flightRequestDTO.flightJourneyRequest == null)
            {
                flightRequestDTO.ErrorStatus = true;
                flightRequestDTO.ErrorMessage = "Please check the payload";
                return flightRequestDTO;
            }
            else if (flightRequestDTO.Adults == 0)
            {
                flightRequestDTO.ErrorStatus = true;
                flightRequestDTO.ErrorMessage = "Please enter the adults";
                return flightRequestDTO;
            }
            flightRequestDTO.ErrorStatus = false;
            return flightRequestDTO;
        }
        public HotelRequestDTO ValidateHotelRequest(HotelRequestDTO hotelRequestDTO)
        {
            if (hotelRequestDTO == null)
            {
                hotelRequestDTO.ErrorStatus = true;
                hotelRequestDTO.ErrorMessage = "Please check the payload";
                return hotelRequestDTO;
            }
            else if (hotelRequestDTO.RoomDetails == null)
            {
                hotelRequestDTO.ErrorStatus = true;
                hotelRequestDTO.ErrorMessage = "Please check the rooms";
                return hotelRequestDTO;
            }
            hotelRequestDTO.ErrorStatus = false;
            return hotelRequestDTO;
        }
        public VisaRequestDTO ValidateVisaRequest(VisaRequestDTO visaRequestDTO)
        {
            if (visaRequestDTO == null)
            {
                visaRequestDTO.ErrorStatus = true;
                visaRequestDTO.ErrorMessage = "Please check the payload";
                return visaRequestDTO;
            }
            visaRequestDTO.ErrorStatus = false;
            return visaRequestDTO;
        }
    }
}
