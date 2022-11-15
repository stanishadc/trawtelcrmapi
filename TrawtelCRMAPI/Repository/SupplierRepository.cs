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
    public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
    {
        public SupplierRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return FindAll()
                .OrderBy(ow => ow.CreatedDate)
                .ToList();
        }
        public Supplier GetSupplierById(Guid SupplierId)
        {
            return FindByCondition(client => client.SupplierId.Equals(SupplierId)).FirstOrDefault();
        }
        public void CreateSupplier(Supplier supplier)
        {
            Create(supplier);
        }
        public void UpdateSupplier(Supplier supplier)
        {
            Update(supplier);
        }
        public void DeleteSupplier(Supplier supplier)
        {
            Delete(supplier);
        }
    }
}
