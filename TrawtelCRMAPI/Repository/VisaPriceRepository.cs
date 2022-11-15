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
    public class VisaPriceRepository : RepositoryBase<VisaPrice>, IVisaPriceRepository
    {
        public VisaPriceRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<VisaPrice> GetAllVisaPrices()
        {
            return FindAll()
                .OrderBy(ow => ow.CreatedDate)
                .ToList();
        }
        public VisaPrice GetVisaPriceById(Guid VisaId)
        {
            return FindByCondition(client => client.VisaPriceId.Equals(VisaId)).FirstOrDefault();
        }
        public void CreateVisaPrice(VisaPrice visa)
        {
            Create(visa);
        }
        public void UpdateVisaPrice(VisaPrice visa)
        {
            Update(visa);
        }
        public void DeleteVisaPrice(VisaPrice visa)
        {
            Delete(visa);
        }
    }
}
