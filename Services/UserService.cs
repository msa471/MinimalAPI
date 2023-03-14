using MinimalAPI.Models;
using MinimalAPI.Repositories;

namespace MinimalAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;       
        public UserService(IUserRepository userRepository) {
            this.userRepository = userRepository;
        } 
        public User Get(UserLogin userLogin) 
        { 
            User user = UserRepository.Users.FirstOrDefault(o => o.Username.Equals(userLogin.Username,
                StringComparison.OrdinalIgnoreCase) && o.Password.Equals(userLogin.Password));

            return user;    
        }

        public User GetUsers(UserLogin userLogin)
        {
            User user = null;
            user = userRepository.UserLogin(userLogin);
            return user;
        }

        public User GetRegistered(UserRegister userRegister)
        {
            User user = null;
            user = userRepository.RegisterUser(userRegister);

            return user;
        }

    }
}