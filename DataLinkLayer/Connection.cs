

using System.Data.SqlClient;

namespace MinimalAPI.DataLinkLayer
{
    public class Connection 
    {
        

        public SqlConnection getConnection()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Server=.;Database=MovieDataBaseUsers;Trusted_Connection=True;";
            return con;
        }
    }
}
