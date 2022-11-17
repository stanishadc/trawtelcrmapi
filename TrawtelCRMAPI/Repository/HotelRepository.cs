﻿using Contracts;
using Entities;
using Entities.Models;
using TripJack;
using static Entities.CommonEnums;

namespace Repository
{
    public class HotelRepository : RepositoryBase<HotelRequest>, IHotelRepository
    {
        RepositoryContext _repositoryContext;
        public HotelRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public IEnumerable<HotelRequest> GetHotelRequestsByAgent(Guid AgentId)
        {
            return FindAll()
                .OrderBy(ow => ow.CreatedDate)
                .ToList();
        }
        public HotelRequest GetHotelRequestById(Guid HotelRequestId)
        {
            return FindByCondition(client => client.HotelRequestId.Equals(HotelRequestId)).FirstOrDefault();
        }
        public IEnumerable<HotelRequest> GetHotelRequestsByClient(Guid ClientId)
        {
            return FindByCondition(client => client.ClientId.Equals(ClientId)).ToList();
        }
        public void CreateHotelRequest(HotelRequest commonHotelRequest)
        {
            Create(commonHotelRequest);
        }
        public void UpdateHotelRequest(HotelRequest commonHotelRequest)
        {
            Update(commonHotelRequest);
        }
        public void DeleteHotelRequest(HotelRequest commonHotelRequest)
        {
            Delete(commonHotelRequest);
        }
    }
}
