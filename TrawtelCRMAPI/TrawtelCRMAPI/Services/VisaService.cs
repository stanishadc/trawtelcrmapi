using Amazon.S3;
using Contracts;
using Entities;
using Entities.Common;
using Entities.Models;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using TrawtelCRMAPI.Helpers;

namespace TrawtelCRMAPI.Services
{
    public class VisaService
    {
        ValidationService _validationService = new ValidationService();
        TravelerService _travelerService;
        private IRepositoryWrapper _repository;
        public VisaService(IRepositoryWrapper repository)
        {
            _repository = repository;
            _travelerService = new TravelerService(_repository);
        }
        public VisaRequestDTO GetVisaRequestDetails(VisaRequestDTO visaRequestDTO)
        {
            visaRequestDTO = _validationService.ValidateVisaRequest(visaRequestDTO);
            if (visaRequestDTO.ErrorStatus)
            {
                return visaRequestDTO;
            }

            return visaRequestDTO;
        }
        public VisaRequestDTO getRequestDTO(VisaRequest VisaRequest)
        {
            VisaRequestDTO VisaRequestDTO = new VisaRequestDTO();
            VisaRequestDTO.VisaRequestId = VisaRequest.VisaRequestId;
            VisaRequestDTO.AgentId = VisaRequest.AgentId;
            VisaRequestDTO.ClientId = VisaRequest.ClientId;
            VisaRequestDTO.TravelDate = VisaRequest.TravelDate;
            VisaRequestDTO.NoOfApplicants = VisaRequest.NoOfApplicants;
            VisaRequestDTO.Status = VisaRequest.Status;
            VisaRequestDTO.CreatedDate = VisaRequest.CreatedDate;
            VisaRequestDTO.UpdatedDate = VisaRequest.UpdatedDate;
            var locationmodel = JsonConvert.DeserializeObject<VisaRequestRoot>(VisaRequest.Location);
            VisaRequestDTO.Location = locationmodel?.location;
            var travelers = JsonConvert.DeserializeObject<string[]>(VisaRequest.Applicants);
            if (travelers?.Length > 0)
            {
                VisaRequestDTO.Applicants = _travelerService.GetApplicants(travelers);
            }
            return VisaRequestDTO;
        }
        public Response<object> SaveVisaRequest(VisaRequestDTO commonVisaRequest, string QueryType)
        {
            Response<object> aPIResponse = new Response<object>();
            try
            {
                VisaRequest VisaRequest = new VisaRequest();
                VisaRequest.AgentId = commonVisaRequest.AgentId;
                VisaRequest.ClientId = commonVisaRequest.ClientId;
                VisaRequest.VisaId = commonVisaRequest.VisaId;
                VisaRequest.TravelDate = commonVisaRequest.TravelDate;
                if (commonVisaRequest.Applicants != null)
                {
                    VisaRequest.NoOfApplicants = commonVisaRequest.Applicants.Count;
                    var apiresponse = _travelerService.ConvertTravelerToStringArray(commonVisaRequest.Applicants);
                    if (apiresponse.Succeeded)
                    {
                        VisaRequest.Applicants = JsonConvert.SerializeObject((string[]?)apiresponse.Data);
                    }
                    else
                    {
                        return apiresponse;
                    }
                }
                else
                {
                    VisaRequest.NoOfApplicants = 0;
                    aPIResponse.Succeeded = false;
                    aPIResponse.Message = "Please enter applicants details";
                    return aPIResponse;
                }
                VisaRequest.Location = JsonConvert.SerializeObject(new { commonVisaRequest.Location });
                if (QueryType == "Save")
                {
                    VisaRequest.VisaRequestId = Guid.NewGuid();
                    VisaRequest.Status = CommonEnums.Status.New.ToString();
                    VisaRequest.CreatedDate = DateTime.UtcNow;
                    VisaRequest.UpdatedDate = DateTime.UtcNow;
                    _repository.VisaRequest.CreateVisaRequest(VisaRequest);
                    aPIResponse.Message = "Visa Request Created";
                }
                else
                {
                    VisaRequest.VisaRequestId = commonVisaRequest.VisaRequestId;
                    VisaRequest.Status = CommonEnums.Status.Replied.ToString();
                    VisaRequest.UpdatedDate = DateTime.UtcNow;
                    _repository.VisaRequest.UpdateVisaRequest(VisaRequest);
                    aPIResponse.Message = "Visa Request Updated";
                }
                _repository.Save();
                aPIResponse.Succeeded = true;
                return aPIResponse;
            }
            catch (Exception ex)
            {
                aPIResponse.Succeeded = false;
                aPIResponse.Message = ex.Message;
                return aPIResponse;
            }
        }
        internal object GetPagination(List<VisaRequestDTO> listRequests, PaginationFilter filter, string? route, IUriService uriService)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = listRequests.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToList();
            var totalRecords = listRequests.Count;
            var pagedReponse = PaginationHelper.CreatePagedReponse<VisaRequestDTO>(pagedData, validFilter, totalRecords, uriService, route);
            return pagedReponse;
        }
    }
}
