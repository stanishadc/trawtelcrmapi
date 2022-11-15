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
    public class AgentRepository : RepositoryBase<Agent>, IAgentRepository
    {
        public AgentRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<Agent> GetAllAgents()
        {
            return FindAll()
                .OrderBy(ow => ow.CreatedDate)
                .ToList();
        }
        public Agent GetAgentById(Guid AgentId)
        {
            return FindByCondition(client => client.AgentId.Equals(AgentId)).FirstOrDefault();
        }
        public void CreateAgent(Agent client)
        {
            Create(client);
        }
        public void UpdateAgent(Agent client)
        {
            Update(client);
        }
        public void DeleteAgent(Agent client)
        {
            Delete(client);
        }
    }
}
