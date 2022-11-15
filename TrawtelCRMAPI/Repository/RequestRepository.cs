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
    public class RequestRepository : RepositoryBase<Request>, IRequestRepository
    {
        public RequestRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<Request> GetAllRequests()
        {
            return FindAll()
                .OrderBy(ow => ow.CreatedDate)
                .ToList();
        }
        public Request GetRequestById(Guid RequestId)
        {
            return FindByCondition(client => client.RequestId.Equals(RequestId)).FirstOrDefault();
        }
        public List<Request> GetRequestByAgentId(Guid AgentId)
        {
            return FindByCondition(client => client.AgentId.Equals(AgentId)).ToList();
        }
        public List<Request> GetRequestByClientId(Guid ClientId)
        {
            return FindByCondition(client => client.ClientId.Equals(ClientId)).ToList();
        }
        public List<Request> GetRequestByTravelType(char TravelType)
        {
            return FindByCondition(client => client.TravelType.Equals(TravelType)).ToList();
        }
        public void CreateRequest(Request client)
        {
            Create(client);
        }
        public void UpdateRequest(Request client)
        {
            Update(client);
        }
        public void DeleteRequest(Request client)
        {
            Delete(client);
        }
    }
}
