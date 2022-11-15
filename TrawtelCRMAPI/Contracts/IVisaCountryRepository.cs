using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IVisaCountryRepository : IRepositoryBase<VisaCountry>
    {
        IEnumerable<VisaCountry> GetAllVisaCountries();
        VisaCountry GetVisaCountryById(Guid visaCountryId);
        void CreateVisaCountry(VisaCountry visaCountry);
        void UpdateVisaCountry(VisaCountry visaCountry);
        void DeleteVisaCountry(VisaCountry visaCountry);
    }
}
