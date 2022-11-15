using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IVisaPriceRepository : IRepositoryBase<VisaPrice>
    {
        IEnumerable<VisaPrice> GetAllVisaPrices();
        VisaPrice GetVisaPriceById(Guid VisaPriceId);
        void CreateVisaPrice(VisaPrice visaPrice);
        void UpdateVisaPrice(VisaPrice visaPrice);
        void DeleteVisaPrice(VisaPrice visaPrice);
    }
}
