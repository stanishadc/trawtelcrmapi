using Entities.Models;

namespace Contracts
{
    public interface IHotelRepository
    {
        void CreateHotelRequest(HotelRequest commonHotelRequest);
        void UpdateHotelRequest(HotelRequest commonHotelRequest);
        void DeleteHotelRequest(HotelRequest commonHotelRequest);
        IEnumerable<HotelRequest> GetHotelRequestsByAgent(Guid AgentId);
        HotelRequest GetHotelRequestById(Guid RequestId);
        IEnumerable<HotelRequest> GetHotelRequestsByClient(Guid ClientId);
    }
}
