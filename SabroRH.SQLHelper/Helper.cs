using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SabroRH.SQLHelper
{
    public class Helper
    {
        private SqlConnection GetConnectiton(string Server, string User, string Password, string Database)
        {
            string connectionString = $"server={Server};database={Database};UID={User};PWD={Password};";

            return new SqlConnection(connectionString);
        }

        public async Task<SqlConnection> Connect(string Server, string User, string Password, string Database)
        { 
           SqlConnection connection = GetConnectiton(Server , User, Password, Database);
           await connection.OpenAsync();

           return connection;
        }

        public async Task<SqlDataReader> ExecuteReader(SqlConnection conn, String sql)
        {
            var command = new SqlCommand(sql, conn);
             return await command.ExecuteReaderAsync(); 
        }
    }
}
