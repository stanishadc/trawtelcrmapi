using Contracts;
using Entities;
using Entities.Models;
using Newtonsoft.Json;

namespace TrawtelCRMAPI.Services
{
    public class TravelerService
    {
        private readonly IRepositoryWrapper _repository;
        public TravelerService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        public APIResponse ValidateTraveler(Traveler traveler)
        {
            APIResponse aPIResponse = new APIResponse();
            if (traveler.TravelerType == null)
            {
                aPIResponse.Status = false;
                aPIResponse.ErrorMessage = "Traveler Type should not be null";
                return aPIResponse;
            }
            if (traveler.TravelerType == CommonEnums.TravellerType.Infant.ToString())
            {
                if (traveler.DateOfBirth != null)
                {
                    var gettraveler = CalculateYourAge((DateTime)traveler.DateOfBirth);
                    if (gettraveler != CommonEnums.TravellerType.Infant.ToString())
                    {
                        aPIResponse.Status = false;
                        aPIResponse.ErrorMessage = "Infant Age should be less than 2 years";
                        return aPIResponse;
                    }
                }
                else
                {
                    aPIResponse.Status = false;
                    aPIResponse.ErrorMessage = "Please enter DateOfBirth for Infant";
                    return aPIResponse;
                }
                aPIResponse.Status = false;
                aPIResponse.ErrorMessage = "Traveler Type should not be null";
                return aPIResponse;
            }
            if (string.IsNullOrEmpty(traveler.FirstName))
            {
                aPIResponse.Status = false;
                aPIResponse.ErrorMessage = "Please Enter the First Name";
                return aPIResponse;
            }
            if (string.IsNullOrEmpty(traveler.LastName))
            {
                aPIResponse.Status = false;
                aPIResponse.ErrorMessage = "Please Enter the Last Name";
                return aPIResponse;
            }
            if (string.IsNullOrEmpty(traveler.Nationality))
            {
                aPIResponse.Status = false;
                aPIResponse.ErrorMessage = "Please Enter the Nationality";
                return aPIResponse;
            }
            aPIResponse.Status = true;
            return aPIResponse;
        }
        public APIResponse SaveTraveler(Traveler traveler)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse = ValidateTraveler(traveler);
            if(!aPIResponse.Status)
            {
                return aPIResponse;
            }
            
            traveler.TravelerId = Guid.NewGuid();
            if (traveler.DateOfBirth == null)
            {
                traveler.DateOfBirth = DateTime.MinValue;
            }
            if (traveler.PassportIssueDate == null)
            {
                traveler.PassportIssueDate = DateTime.MinValue;
            }
            if (traveler.PassportExpireDate == null)
            {
                traveler.PassportExpireDate = DateTime.MinValue;
            }
            traveler.CreatedDate = DateTime.UtcNow;
            traveler.UpdatedDate = DateTime.UtcNow;
            Guid travelerId = _repository.Traveler.CheckTravelerExists(traveler);
            if (travelerId == default(Guid))
            {
                _repository.Traveler.CreateTraveler(traveler);
                _repository.Save();
                aPIResponse.Status = true;
                aPIResponse.Data = traveler.TravelerId.ToString();
            }
            else
            {
                aPIResponse.Status = true;
                aPIResponse.Data = travelerId.ToString();
            }
            return aPIResponse;
        }
        public List<Traveler>? GetApplicants(string[]? travelers)
        {
            List<Traveler> travelerslist = new List<Traveler>();
            for (int i = 0; i < travelers.Length; i++)
            {
                var travelerId = new Guid(travelers[i]);
                var traveler = _repository.Traveler.GetTravelerById(travelerId);
                if (traveler != null)
                {
                    travelerslist.Add(traveler);
                }
            }
            return travelerslist;
        }
        public APIResponse ConvertTravelerToStringArray(List<Traveler> applicants)
        {
            APIResponse aPIResponse = new APIResponse();
            List<string> travelers = new List<string>();
            var adultapplicants = applicants.Where(t => t.TravelerType == CommonEnums.TravellerType.Adult.ToString()).ToList();
            if(adultapplicants.Count == 0)
            {
                aPIResponse.Status = false;
                aPIResponse.ErrorMessage = "Atleast One Adult is Mandatory";
                return aPIResponse;
            }
            foreach (Traveler traveler in applicants)
            {
                var response = SaveTraveler(traveler);
                if (response.Status)
                {
                    travelers.Add(response.Data.ToString());
                }
                else
                {
                    return response;
                }
            }
            aPIResponse.Status = true;
            aPIResponse.Data = travelers.ToArray();
            return aPIResponse;
        }
        private string CalculateYourAge(DateTime Dob)
        {
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            if (Years >= 18)
            {
                return CommonEnums.TravellerType.Adult.ToString();
            }
            else if (Years >= 2)
            {
                return CommonEnums.TravellerType.Kid.ToString();
            }
            else
            {
                return CommonEnums.TravellerType.Infant.ToString();
            }
        }
    }
}
