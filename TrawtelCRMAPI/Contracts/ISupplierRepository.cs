using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISupplierRepository : IRepositoryBase<Supplier>
    {
        IEnumerable<Supplier> GetAllSuppliers();
        Supplier GetSupplierById(Guid SupplierId);
        void CreateSupplier(Supplier supplier);
        void UpdateSupplier(Supplier supplier);
        void DeleteSupplier(Supplier supplier);
    }
}
