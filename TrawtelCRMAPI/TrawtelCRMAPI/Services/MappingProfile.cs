using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace TrawtelCRMAPI.Services
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<Visa, VisaDTO>();
            CreateMap<VisaCountry, VisaCountryDTO>();
            CreateMap<VisaPrice, VisaPriceDTO>();
            CreateMap<Agent, AgentDTO>();
            CreateMap<Supplier, SupplierDTO>();
            CreateMap<Client, ClientDTO>();
            CreateMap<SupplierCode,SupplierCodeDTO>();
        }
    }
}
