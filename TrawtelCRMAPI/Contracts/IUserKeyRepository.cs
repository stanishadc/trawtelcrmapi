using Entities.Models;

namespace Contracts
{
    public interface IUserKeyRepository : IRepositoryBase<UserKey>
    {
        UserKey GetUserKeyByAgentId(Guid AgentId);
        void CreateUserKey(UserKey user);
        void UpdateUserKey(UserKey user);
        void DeleteUserKey(UserKey user);
        bool CheckValidUserkey(string APIKEY);
    }
}
