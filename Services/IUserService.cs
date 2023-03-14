using MinimalAPI.Models;

namespace MinimalAPI.Services
{
    public interface IUserService
    {
        public User Get(UserLogin userLogin);

        public User GetUsers(UserLogin userLogin);

        public User GetRegistered(UserRegister userRegister); 
    }
}
