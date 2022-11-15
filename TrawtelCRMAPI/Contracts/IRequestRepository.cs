using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRequestRepository: IRepositoryBase<Request>
    {
        IEnumerable<Request> GetAllRequests();
        Request GetRequestById(Guid RequestId);
        List<Request> GetRequestByAgentId(Guid AgentId);
        List<Request> GetRequestByClientId(Guid ClientId);
        List<Request> GetRequestByTravelType(char TravelType);
        void CreateRequest(Request request);
        void UpdateRequest(Request request);
        void DeleteRequest(Request request);
    }
}
