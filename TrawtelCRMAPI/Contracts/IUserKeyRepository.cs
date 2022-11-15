using Entities.Models;

namespace Contracts
{
    public interface IUserKeyRepository : IRepositoryBase<UserKey>
    {
        IEnumerable<UserKey> GetAllUserKeys();
        UserKey GetUserKeyById(Guid UserKeyId);
        void CreateUserKey(UserKey user);
        void UpdateUserKey(UserKey user);
        void DeleteUserKey(UserKey user);
    }
}
