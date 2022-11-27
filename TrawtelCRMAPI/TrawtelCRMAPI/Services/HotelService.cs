using Amazon.S3;
using Contracts;
using Entities;
using Entities.Common;
using Entities.Models;
using Newtonsoft.Json;
using TrawtelCRMAPI.Helpers;

namespace TrawtelCRMAPI.Services
{
    public class HotelService
    {
        ValidationService _validationService = new ValidationService();
        TravelerService _travelerService;
        private IRepositoryWrapper _repository;
        public HotelService(IRepositoryWrapper repository)
        {
            _repository = repository;
            _travelerService = new TravelerService(_repository);
        }
        public Response<object> SaveHotelRequest(HotelRequestDTO commonHotelRequest,string QueryType)
        {
            Response<object> aPIResponse = new Response<object>();
            try
            {
                HotelRequest HotelRequest = new HotelRequest();
                HotelRequest.AgentId = commonHotelRequest.AgentId;
                HotelRequest.ClientId = commonHotelRequest.ClientId;
                HotelRequest.CheckIn = commonHotelRequest.CheckIn;
                HotelRequest.CheckOut = commonHotelRequest.CheckOut;

                if (commonHotelRequest.RoomDetails != null)
                {
                    List<RoomGuestDetails> roomGuestDetailsList = new List<RoomGuestDetails>();
                    for (int i = 0; i < commonHotelRequest.RoomDetails.Count; i++)
                    {
                        RoomGuestDetails roomGuestDetails = new RoomGuestDetails();
                        var roomDetails = commonHotelRequest.RoomDetails[i];
                        var apiresponse = _travelerService.ConvertTravelerToStringArray(roomDetails.GuestDetails);
                        if (apiresponse.Succeeded)
                        {
                            roomGuestDetails.Adults = roomDetails.Adults;
                            roomGuestDetails.KidsAge = roomDetails.KidsAge;
                            roomGuestDetails.GuestDetails = (string[]?)apiresponse.Data;
                            roomGuestDetailsList.Add(roomGuestDetails);
                        }
                        else
                        {
                            return apiresponse;
                        }
                    }
                    HotelRequest.RoomDetails = JsonConvert.SerializeObject(roomGuestDetailsList);
                }
                HotelRequest.Location = JsonConvert.SerializeObject(new { commonHotelRequest.Location });

                if (QueryType == "Save")
                {
                    HotelRequest.HotelRequestId = Guid.NewGuid();
                    HotelRequest.Status = CommonEnums.Status.New.ToString();
                    HotelRequest.CreatedDate = DateTime.UtcNow;
                    HotelRequest.UpdatedDate = DateTime.UtcNow;
                    _repository.Hotel.CreateHotelRequest(HotelRequest);
                    aPIResponse.Message = "Hotel Request Created";
                }
                else
                {
                    HotelRequest.HotelRequestId = commonHotelRequest.HotelRequestId;
                    HotelRequest.Status = CommonEnums.Status.Replied.ToString();
                    HotelRequest.UpdatedDate = DateTime.UtcNow;
                    _repository.Hotel.UpdateHotelRequest(HotelRequest);
                    aPIResponse.Message = "Hotel Request Updated";
                }
                _repository.Save();
                aPIResponse.Succeeded = true;
            }
            catch(Exception ex)
            {
                aPIResponse.Succeeded = false;
                aPIResponse.Message = ex.Message;
            }
            return aPIResponse;
        }
        public HotelRequestDTO getRequestDTO(HotelRequest HotelRequest)
        {
            HotelRequestDTO HotelRequestDTO = new HotelRequestDTO();
            HotelRequestDTO.HotelRequestId = HotelRequest.HotelRequestId;
            HotelRequestDTO.AgentId = HotelRequest.AgentId;
            HotelRequestDTO.ClientId = HotelRequest.ClientId;
            HotelRequestDTO.CheckIn = HotelRequest.CheckIn;
            HotelRequestDTO.CheckOut = HotelRequest.CheckOut;
            HotelRequestDTO.Status = HotelRequest.Status;
            HotelRequestDTO.CreatedDate = HotelRequest.CreatedDate;
            HotelRequestDTO.UpdatedDate = HotelRequest.UpdatedDate;
            var roomGuestDetails = JsonConvert.DeserializeObject<List<RoomGuestDetails>>(HotelRequest.RoomDetails);
            List<RoomDetails> roomdetailsList = new List<RoomDetails>();

            for (int i = 0; i < roomGuestDetails.Count; i++)
            {
                RoomDetails roomDetails = new RoomDetails();
                roomDetails.Adults = roomGuestDetails[i].Adults;
                roomDetails.KidsAge = roomGuestDetails[i].KidsAge;
                var guests = _travelerService.GetApplicants(roomGuestDetails[i].GuestDetails);
                roomDetails.GuestDetails = guests;
                roomdetailsList.Add(roomDetails);
            }
            HotelRequestDTO.RoomDetails = roomdetailsList;
            var locationmodel = JsonConvert.DeserializeObject<HotelRequestRoot>(HotelRequest.Location);
            HotelRequestDTO.Location = locationmodel?.location;

            return HotelRequestDTO;
        }
        public HotelRequestDTO GetHotelRequestDetails(HotelRequestDTO hotelRequestDTO)
        {
            hotelRequestDTO = _validationService.ValidateHotelRequest(hotelRequestDTO);
            if (hotelRequestDTO.ErrorStatus)
            {
                return hotelRequestDTO;
            }
            return hotelRequestDTO;
        }
        internal object GetPagination(List<HotelRequestDTO> listRequests, PaginationFilter filter, string? route, IUriService uriService)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = listRequests.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToList();
            var totalRecords = listRequests.Count;
            var pagedReponse = PaginationHelper.CreatePagedReponse<HotelRequestDTO>(pagedData, validFilter, totalRecords, uriService, route);
            return pagedReponse;
        }
    }
}