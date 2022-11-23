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
        public UserKey GetUserKeyByAgentId(Guid AgentId)
        {
            return FindByCondition(user => user.AgentId.Equals(AgentId)).FirstOrDefault();
        }
        public bool CheckValidUserkey(string APIKEY)
        {
            var response = FindByCondition(user => user.SecretKey.Equals(APIKEY) && user.Status == "0").FirstOrDefault();
            if (response == null)
            {
                return false;
            }
            else
            {
                return true;
            }
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
