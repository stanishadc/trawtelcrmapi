using Amazon.S3;
using Entities.Models;

namespace TrawtelCRMAPI.Services
{
    public class HotelService
    {
        private readonly AmazonService _amazonService;
        public HotelService(IAmazonS3 s3Client)
        {
            _amazonService = new AmazonService(s3Client);
        }
        public HotelRequestDTO ValidateHotelRequest(HotelRequestDTO hotelRequestDTO)
        {
            if (hotelRequestDTO == null)
            {
                hotelRequestDTO.ErrorStatus = true;
                hotelRequestDTO.ErrorMessage = "Please check the payload";
            }
            else if (hotelRequestDTO.RoomDetails == null)
            {
                hotelRequestDTO.ErrorStatus = true;
                hotelRequestDTO.ErrorMessage = "Please check the rooms";
            }
            return hotelRequestDTO;
        }
        public HotelRequestDTO GetHotelRequestDetails(HotelRequestDTO hotelRequestDTO)
        {
            hotelRequestDTO = ValidateHotelRequest(hotelRequestDTO);
            if (hotelRequestDTO.ErrorStatus)
            {
                return hotelRequestDTO;
            }

            return hotelRequestDTO;
        }
    }
}
