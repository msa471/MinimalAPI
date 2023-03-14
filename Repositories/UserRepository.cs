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
        private readonly AppDbContext _context;


        public UserRepository(AppDbContext context)
        {
            _context = context; 
        }

        public static List<User> Users = new()
        {
            new(){Username = "admin", Email="admin@email.com",Password="adminPassword",Role="Administrator"},
            new(){Username = "Siraaj", Email="Siraaj@email.com",Password="siraajPassword",Role="Standard"},
        };

        public User UserLogin(UserLogin userLogin) {
            try
            {
                Connection connection = new Connection();
                SqlCommand validateCmd = new SqlCommand();
                var sqlConnect = connection.getConnection();

                using (sqlConnect)
                {
                    sqlConnect.Open();
                    var query = string.Format("SELECT * FROM dbo.Users WHERE Username = '{0}' and Password = '{1}'",userLogin.Username,userLogin.Password);
                    validateCmd = new SqlCommand(query, sqlConnect);

                    SqlDataReader reader = validateCmd.ExecuteReader();

                    while (reader.Read())
                    { 
                        //retrieve data
                        var col1User = reader.GetValue(0);
                        var col2Email = reader.GetValue(1);
                        var col3Password = reader.GetValue(2);
                        var col4Role = reader.GetValue(3);

                        var returnUser = new User();


                        returnUser.Username = col1User.ToString();
                        returnUser.Email = col2Email.ToString();
                        returnUser.Password = col3Password.ToString();
                        returnUser.Role = col4Role.ToString();


                        return returnUser;
                    }
                }
                //validateCmd.ExecuteNonQuery();
                
            }
            catch (Exception e)
            {
                // ...handle, rethrow. Also, you might want to catch
                // more specific exceptions...
            }

            return null;
        }
        public User RegisterUser(UserRegister userRegister)
        {
           
            try
            {
                var allUsers = _context.Users.ToListAsync();

                /* Connection connection = new Connection();
                 SqlCommand cmd = new SqlCommand();
                 var sqlConnect = connection.getConnection();

                 using (sqlConnect)
                 {
                     sqlConnect.Open();
                     cmd = new SqlCommand("insert into dbo.Users(Username,Email,Password,Role)values('"+userRegister.Username+"','"+userRegister.Email+"','"+userRegister.Password+ "','"+userRegister.Role+"')", sqlConnect);
                     cmd.ExecuteNonQuery();
                     sqlConnect.Close(); 
                 }*/
            }
            catch (Exception e)
            {
                // ...handle, rethrow. Also, you might want to catch
                // more specific exceptions...
            }
            var returnUser = new User();
          

            returnUser.Username = userRegister.Username;        
            returnUser.Email = userRegister.Email;
            returnUser.Password = userRegister.Password;
            returnUser.Role = userRegister.Role;    

    
            return returnUser;
        }
    }
}
