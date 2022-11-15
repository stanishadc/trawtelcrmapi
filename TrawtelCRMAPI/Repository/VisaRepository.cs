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
    public class VisaRepository : RepositoryBase<Visa>, IVisaRepository
    {
        public VisaRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<Visa> GetAllVisas()
        {
            return FindAll()
                .OrderBy(ow => ow.CreatedDate)
                .ToList();
        }
        public Visa GetVisaById(Guid VisaId)
        {
            return FindByCondition(client => client.VisaId.Equals(VisaId)).FirstOrDefault();
        }
        public List<Visa> GetVisasByCountryName(string? VisaCountryName)
        {
            return FindByCondition(client => client.VisaCountry.Name.Equals(VisaCountryName)).ToList();
        }        
        public List<Visa> GetVisasByCountryId(string VisaCountryId)
        {
            return FindByCondition(client => client.VisaCountry.VisaCountryId.Equals(VisaCountryId)).ToList();
        }
        public void CreateVisa(Visa visa)
        {
            Create(visa);
        }
        public void UpdateVisa(Visa visa)
        {
            Update(visa);
        }
        public void DeleteVisa(Visa visa)
        {
            Delete(visa);
        }
    }
}
