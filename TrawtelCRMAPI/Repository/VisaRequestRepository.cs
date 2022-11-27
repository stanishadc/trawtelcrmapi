using Contracts;
using Entities;
using Entities.Common;
using Entities.Models;

namespace Repository
{
    internal class VisaRequestRepository : RepositoryBase<VisaRequest>, IVisaRequestRepository
    {
        RepositoryContext _repositoryContext;
        public VisaRequestRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public IEnumerable<VisaRequest> GetVisaRequestsByAgent(Guid AgentId)
        {
            return FindByCondition(client => client.AgentId.Equals(AgentId)).ToList();
        }
        public IEnumerable<VisaRequest> GetVisaRequestsByStatus(Guid AgentId, string Status)
        {
            return FindByCondition(client => client.AgentId.Equals(AgentId) && client.Status.Equals(Status)).ToList();
        }
        public VisaRequest GetVisaRequestById(Guid VisaRequestId)
        {
            return FindByCondition(client => client.VisaRequestId.Equals(VisaRequestId)).FirstOrDefault();
        }
        public IEnumerable<VisaRequest> GetVisaRequestsByClient(Guid ClientId)
        {
            return FindByCondition(client => client.ClientId.Equals(ClientId)).ToList();
        }
        public void CreateVisaRequest(VisaRequest commonVisaRequest)
        {
            Create(commonVisaRequest);
        }
        public void UpdateVisaRequest(VisaRequest commonVisaRequest)
        {
            Update(commonVisaRequest);
        }
        public void DeleteVisaRequest(VisaRequest commonVisaRequest)
        {
            Delete(commonVisaRequest);
        }
    }
}