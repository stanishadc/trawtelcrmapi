using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITravelerRepository : IRepositoryBase<Traveler>
    {
        IEnumerable<Traveler> GetAllTravelers();
        Traveler GetTravelerById(Guid TravelerId);
        IEnumerable<Traveler> GetTravelerByAgentId(Guid AgentId);
        IEnumerable<Traveler> GetTravelerBirthdaysByAgentId(Guid AgentId);
        void CreateTraveler(Traveler traveler);
        void UpdateTraveler(Traveler traveler);
        void DeleteTraveler(Traveler traveler);
    }
}

