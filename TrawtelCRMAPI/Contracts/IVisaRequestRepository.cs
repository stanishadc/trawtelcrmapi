using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IVisaRequestRepository : IRepositoryBase<VisaRequest>
    {
        void CreateVisaRequest(VisaRequest commonVisaRequest);
        void UpdateVisaRequest(VisaRequest commonVisaRequest);
        void DeleteVisaRequest(VisaRequest commonVisaRequest);
        IEnumerable<VisaRequest> GetVisaRequestsByAgent(Guid AgentId);
        VisaRequest GetVisaRequestById(Guid RequestId);
        IEnumerable<VisaRequest> GetVisaRequestsByClient(Guid ClientId);
        IEnumerable<VisaRequest> GetVisaRequestsByStatus(Guid AgentId, string Status);
    }
}
