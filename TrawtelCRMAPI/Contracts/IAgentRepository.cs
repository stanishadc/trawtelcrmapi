using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAgentRepository : IRepositoryBase<Agent>
    {
        IEnumerable<Agent> GetAllAgents();
        Agent GetAgentById(Guid AgentId);
        Agent GetAgentByUserId(Guid UserId);
        void CreateAgent(Agent agent);
        void UpdateAgent(Agent agent);
        void DeleteAgent(Agent agent);
    }
}
