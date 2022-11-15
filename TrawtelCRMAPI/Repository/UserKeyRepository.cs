using Contracts;
using Entities.Models;
using Entities;

namespace Repository
{
    public class UserKeyRepository : RepositoryBase<UserKey>, IUserKeyRepository
    {
        public UserKeyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<UserKey> GetAllUserKeys()
        {
            return FindAll()
                .OrderBy(ow => ow.CreatedDate)
                .ToList();
        }
        public UserKey GetUserKeyById(Guid UserKeyId)
        {
            return FindByCondition(user => user.UserKeyId.Equals(UserKeyId)).FirstOrDefault();
        }
        public void CreateUserKey(UserKey user)
        {
            Create(user);
        }
        public void UpdateUserKey(UserKey user)
        {
            Update(user);
        }
        public void DeleteUserKey(UserKey user)
        {
            Delete(user);
        }
    }
}
