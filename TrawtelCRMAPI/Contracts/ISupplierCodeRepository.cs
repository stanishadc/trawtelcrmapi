using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISupplierCodeRepository : IRepositoryBase<SupplierCode>
    {
        IEnumerable<SupplierCode> GetAllSupplierCodes();
        SupplierCode GetSupplierCodeById(Guid SupplierCodeId);
        void CreateSupplierCode(SupplierCode supplierCode);
        void UpdateSupplierCode(SupplierCode supplierCode);
        void DeleteSupplierCode(SupplierCode supplierCode);
        List<AgentSuppliers?> GetDefaultSupplierByAgentId(Guid AgentId, string? TravelType);
    }
}
