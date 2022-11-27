using Contracts;
using Entities;
using Entities.Common;
using Entities.DataTransferObjects;
using Entities.Models;
using Newtonsoft.Json;
using TrawtelCRMAPI.Helpers;

namespace TrawtelCRMAPI.Services
{
    public class RequestService
    {
        private IRepositoryWrapper _repository;
        public RequestService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }        
        public List<RequestDTO> getRequestByStatus(Guid AgentId, string Status)
        {
            List<RequestDTO> requestlist = new List<RequestDTO>();

            var flightslist = _repository.Flight.GetFlightRequestsByStatus(AgentId, Status);
            foreach (var flight in flightslist)
            {
                requestlist.Add(getFlightRequest(flight));
            }
            var hotelslist = _repository.Hotel.GetHotelRequestsByStatus(AgentId, Status);
            foreach (var hotel in hotelslist)
            {                
                requestlist.Add(getHotelRequest(hotel));
            }
            var visaslist = _repository.VisaRequest.GetVisaRequestsByStatus(AgentId, Status);
            foreach (var visa in visaslist)
            {
                requestlist.Add(getVisaRequest(visa));
            }
            return requestlist;
        }
        public List<RequestDTO> getRequests(Guid AgentId)
        {
            List<RequestDTO> requestlist = new List<RequestDTO>();

            var flightslist = _repository.Flight.GetFlightRequestsByAgent(AgentId);
            foreach (var flight in flightslist)
            {
                requestlist.Add(getFlightRequest(flight));
            }
            var hotelslist = _repository.Hotel.GetHotelRequestsByAgent(AgentId);
            foreach (var hotel in hotelslist)
            {
                requestlist.Add(getHotelRequest(hotel));
            }
            var visaslist = _repository.VisaRequest.GetVisaRequestsByAgent(AgentId);
            foreach (var visa in visaslist)
            {
                requestlist.Add(getVisaRequest(visa));
            }
            return requestlist;
        }
        private RequestDTO getFlightRequest(FlightRequest flight)
        {
            RequestDTO requestDTO = new RequestDTO();
            requestDTO.RequestId = flight.FlightRequestId;
            requestDTO.AgentId = flight.AgentId;
            requestDTO.Status = flight.TravelDate <= DateTime.UtcNow ? CommonEnums.Status.Expired.ToString() : flight.Status;
            requestDTO.TravelDate = flight.TravelDate;
            requestDTO.ClientId = flight.ClientId;
            requestDTO.TravelType = CommonEnums.TravelType.Flight.ToString();
            requestDTO.CreatedDate = flight.CreatedDate;
            string adults = flight.Adults == 1 ? flight.Adults + " Adult," : flight.Adults + " Adults,";
            string kids = flight.Kids == 1 ? flight.Kids + " Kid," : flight.Kids + " Kids,";
            string infants = flight.Infants == 1 ? flight.Infants + " Infant" : flight.Infants + " Infants";
            requestDTO.Travelers = adults + kids + infants;
            var trmodel = JsonConvert.DeserializeObject<FlightRequestRoot>(flight.flightJourneyRequest);
            var flightJourneyRequest = trmodel?.flightJourneyRequest;
            if (flightJourneyRequest != null)
            {
                if (flight.JourneyType == CommonEnums.JourneyTypes.OneWay.ToString())
                {
                    for (int i = 0; i < flightJourneyRequest.Count; i++)
                    {
                        requestDTO.Location = flightJourneyRequest[i].LocationFrom.City + "," + flightJourneyRequest[i].LocationFrom.Country + " - " + flightJourneyRequest[i].LocationTo.City + "," + flightJourneyRequest[i].LocationTo.Country;
                        if (i > 1)
                        {
                            requestDTO.Location = requestDTO.Location + " - " + flightJourneyRequest[i].LocationTo.City + "," + flightJourneyRequest[i].LocationTo.Country;
                        }
                    }
                }
                else
                {
                    requestDTO.Location = flightJourneyRequest[0].LocationFrom.City + "," + flightJourneyRequest[0].LocationFrom.Country + " - " + flightJourneyRequest[0].LocationTo.City + "," + flightJourneyRequest[0].LocationTo.Country;
                }
            }
            return requestDTO;
        }
        private RequestDTO getHotelRequest(HotelRequest hotel)
        {
            RequestDTO requestDTO = new RequestDTO();
            requestDTO.RequestId = hotel.HotelRequestId;
            requestDTO.AgentId = hotel.AgentId;
            requestDTO.Status = hotel.CheckIn <= DateTime.UtcNow ? CommonEnums.Status.Expired.ToString() : hotel.Status;
            requestDTO.TravelDate = hotel.CheckIn;
            requestDTO.ClientId = hotel.ClientId;
            requestDTO.TravelType = CommonEnums.TravelType.Hotel.ToString();
            requestDTO.CreatedDate = hotel.CreatedDate;
            var roomGuestDetails = JsonConvert.DeserializeObject<List<RoomGuestDetails>>(hotel.RoomDetails);
            int adults = 0, kids = 0;
            for (int i = 0; i < roomGuestDetails.Count; i++)
            {
                adults = adults + roomGuestDetails[i].Adults;
                kids = kids + roomGuestDetails[i].KidsAge.Length;
            }
            string rooms = roomGuestDetails.Count == 1 ? roomGuestDetails.Count + " Room," : roomGuestDetails.Count + " Rooms,";
            string roomadults = adults == 1 ? adults + " Adult," : adults + " Adults,";
            string roomkids = kids == 1 ? kids + " Kid," : kids + " Kids,";

            requestDTO.Travelers = rooms + roomadults + roomkids;
            var locationmodel = JsonConvert.DeserializeObject<HotelRequestRoot>(hotel.Location);
            requestDTO.Location = locationmodel?.location.City + "," + locationmodel?.location.Country;
            return requestDTO;
        }
        private RequestDTO getVisaRequest(VisaRequest visa)
        {
            RequestDTO requestDTO = new RequestDTO();
            requestDTO.RequestId = visa.VisaRequestId;
            requestDTO.AgentId = visa.AgentId;
            requestDTO.Status = visa.TravelDate <= DateTime.UtcNow ? CommonEnums.Status.Expired.ToString() : visa.Status;
            requestDTO.TravelDate = visa.TravelDate;
            requestDTO.ClientId = visa.ClientId;
            requestDTO.TravelType = CommonEnums.TravelType.Visa.ToString();
            requestDTO.CreatedDate = visa.CreatedDate;
            string applicants = visa.NoOfApplicants == 1 ? visa.NoOfApplicants + " Applicant" : visa.NoOfApplicants + " Applicants";
            requestDTO.Travelers = applicants;
            var locationmodel = JsonConvert.DeserializeObject<HotelRequestRoot>(visa.Location);
            requestDTO.Location = locationmodel?.location.City + "," + locationmodel?.location.Country;
            return requestDTO;
        }
        internal object GetPagination(List<RequestDTO> listRequests, PaginationFilter filter, string? route, IUriService uriService)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = listRequests.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToList();
            var totalRecords = listRequests.Count;
            var pagedReponse = PaginationHelper.CreatePagedReponse<RequestDTO>(pagedData, validFilter, totalRecords, uriService, route);
            return pagedReponse;
        }
    }
}
