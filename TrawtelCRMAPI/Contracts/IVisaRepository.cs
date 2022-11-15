using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IVisaRepository : IRepositoryBase<Visa>
    {
        IEnumerable<Visa> GetAllVisas();
        Visa GetVisaById(Guid VisaId);
        void CreateVisa(Visa visa);
        void UpdateVisa(Visa visa);
        void DeleteVisa(Visa visa);
        List<Visa> GetVisasByCountryName(string VisaCountryName);
        List<Visa> GetVisasByCountryId(string VisaCountryId);
    }
}
