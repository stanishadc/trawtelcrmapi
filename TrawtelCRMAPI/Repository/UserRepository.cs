using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<User> GetAllUsers()
        {
            return FindAll()
                .OrderBy(ow => ow.UserName)
                .ToList();
        }
        public User GetUserById(Guid UserId)
        {
            return FindByCondition(user => user.UserId.Equals(UserId)).FirstOrDefault();
        }
        public void CreateUser(User user)
        {
            Create(user);
        }
        public void UpdateUser(User user)
        {
            Update(user);
        }
        public void DeleteUser(User user)
        {
            Delete(user);
        }
    }
}
