using Contracts;
using Entities;
using Entities.Common;
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
        public Response<User> CheckLogin(string? UserName, string? Password)
        {
            Response<User> response = new Response<User>();
            var user = FindByCondition(client => client.UserName.Equals(UserName)).FirstOrDefault();
            if (user == null)
            {
                response.Succeeded = false;
                response.Message = "No user exists with this email";
                return response;
            }
            if (user.UserName == UserName && user.Password == Password)
            {
                if (!string.IsNullOrEmpty(user.Status))
                {
                    if (Convert.ToInt32(user.Status) == (int)CommonEnums.UserStatus.Active)
                    {
                        response.Succeeded = true;
                        response.Data = user;
                        return response;
                    }
                    else
                    {
                        response.Succeeded = false;
                        response.Message = "Your account is not active. Please check with Administrator";
                        return response;
                    }
                }
            }
            else
            {
                response.Succeeded = false;
                response.Message = "Please check the credentials";
                return response;
            }
            return response;
        }
    }
}
