using MinimalAPI.Models;

namespace MinimalAPI.Repositories
{
    public interface IUserRepository
    {
        User UserLogin(UserLogin userLogin);
        User RegisterUser(UserRegister userRegister);
    }
}
