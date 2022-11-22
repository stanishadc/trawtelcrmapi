using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Runtime.ExceptionServices;

namespace Repository
{
    public class TravelerRepository : RepositoryBase<Traveler>, ITravelerRepository
    {
        public TravelerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<Traveler> GetAllTravelers()
        {
            return FindAll()
                .OrderBy(ow => ow.CreatedDate)
                .ToList();
        }
        public Traveler GetTravelerById(Guid ClientId)
        {
            return FindByCondition(client => client.TravelerId.Equals(ClientId)).FirstOrDefault();
        }
        public IEnumerable<Traveler> GetTravelerByAgentId(Guid AgentId)
        {
            return FindByCondition(client => client.AgentId.Equals(AgentId))
                .ToList();
        }
        public IEnumerable<Traveler> GetTravelerBirthdaysByAgentId(Guid AgentId)
        {
            return FindByCondition(client => client.AgentId.Equals(AgentId)).OrderBy(ow => ow.DateOfBirth).ToList();
        }
        public void CreateTraveler(Traveler traveler)
        {
            Create(traveler);
        }
        public void UpdateTraveler(Traveler traveler)
        {
            Update(traveler);
        }
        public void DeleteTraveler(Traveler traveler)
        {
            Delete(traveler);
        }
        public Guid CheckTravelerExists(Traveler traveler)
        {
            var traveller = FindByCondition(client => client.FirstName.Equals(traveler.FirstName) && client.LastName.Equals(traveler.LastName) && client.DateOfBirth.Equals(traveler.DateOfBirth))
                .FirstOrDefault();
            if (traveller == null)
            {
                return default(Guid);
            }
            else
            {
                return traveller.TravelerId;
            }
        }
    }
}

