using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.DataLinkLayer;
using MinimalAPI.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MinimalAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;


        public UserRepository(UserDbContext context)
        {
            _context = context; 
        }

        public static List<User> Users = new()
        {
            new(){Username = "admin", Email="admin@email.com",Password="adminPassword",Role="Administrator"},
            new(){Username = "Siraaj", Email="Siraaj@email.com",Password="siraajPassword",Role="Standard"},
        };

        public User UserLogin(UserLogin userLogin) {

            var u = _context.Users.Where(x => x.Username == userLogin.Username && x.Password == userLogin.Password);  

            return u.FirstOrDefault();
        }
        public User RegisterUser(UserRegister userRegister)
        {
            User user = new User();
            try
            {
                user = new User
                {
                        Username = userRegister.Username,
                        Password = userRegister.Password,
                        Email = userRegister.Email,
                        Role = userRegister.Role
                    };
                
                    _context.Users.Add(user);
                    _context.SaveChanges(); 
            }
            catch (SqlException exc)
            {
                // ...handle, rethrow. Also, you might want to catch
                // more specific exceptions...
               
            }
            return user;
            
        }
    }
}
