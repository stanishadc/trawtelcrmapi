using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class VisaCountryRepository : RepositoryBase<VisaCountry>, IVisaCountryRepository
    {
        public VisaCountryRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<VisaCountry> GetAllVisaCountries()
        {
            return FindAll()
                .OrderBy(ow => ow.VisaCountryId)
                .ToList();
        }
        public VisaCountry GetVisaCountryById(Guid VisaId)
        {
            return FindByCondition(client => client.VisaCountryId.Equals(VisaId)).FirstOrDefault();
        }
        public void CreateVisaCountry(VisaCountry visa)
        {
            Create(visa);
        }
        public void UpdateVisaCountry(VisaCountry visa)
        {
            Update(visa);
        }
        public void DeleteVisaCountry(VisaCountry visa)
        {
            Delete(visa);
        }
    }
}
