using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class SupplierCodeRepository : RepositoryBase<SupplierCode>, ISupplierCodeRepository
    {
        RepositoryContext _repositoryContext;
        public SupplierCodeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public IEnumerable<SupplierCode> GetAllSupplierCodes()
        {
            return FindAll()
                .OrderBy(ow => ow.CreatedDate)
                .ToList();
        }
        public SupplierCode GetSupplierCodeById(Guid SupplierCodeId)
        {
            return FindByCondition(client => client.SupplierCodeId.Equals(SupplierCodeId)).FirstOrDefault();
        }
        public void CreateSupplierCode(SupplierCode supplierCode)
        {
            Create(supplierCode);
        }
        public void UpdateSupplierCode(SupplierCode supplierCode)
        {
            Update(supplierCode);
        }
        public void DeleteSupplierCode(SupplierCode supplierCode)
        {
            Delete(supplierCode);
        }
        public List<AgentSuppliers>? GetDefaultSupplierByAgentId(Guid AgentId, string? TravelType)
        {
            var agentsuppliers = FindByCondition(client => client.AgentId.Equals(AgentId)).ToList();

            if (agentsuppliers.Count > 0)
            {
                agentsuppliers = agentsuppliers.Where(o => o.TravelType == TravelType).ToList();
                var response = (from sc in agentsuppliers
                                join s in _repositoryContext.Suppliers on sc.SupplierId equals s.SupplierId
                                select new AgentSuppliers { SupplierId = sc.SupplierId, SupplierName = s.Name, SupplierKey = sc.TestAPIKey, SupplierURL = sc.TestURL, SupplierUserName = sc.TestUserName, SupplierPassword = sc.TestPassword }).ToList();
                if (response != null)
                {
                    return response;
                }
            }
            return null;
        }
    }
}

