using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallStore
{
    class Database
    {
        const string CONN_STRING = @"Data Source=ipd8.database.windows.net;Initial Catalog=store;Integrated Security=False;User ID=ipd8abbott;Password=Abbott2000;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SqlConnection conn;
        public Database()
        {
            conn = new SqlConnection(CONN_STRING);
            conn.Open();//it may throw exception
        }
        public bool LoginAdminToStore(Employee e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Employee WHERE UserName=@Username and Password=@Password and IsManager=1", conn);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@Username", e.UserName);
            cmd.Parameters.AddWithValue("@Password", e.UserName);


            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                int rowCount=0;

                if (reader.HasRows)
                {
                    rowCount = 0;
                    while (reader.Read())
                    {
                        rowCount++;
                    }
                    
                }
                if (rowCount == 1) return true;
                return false;
            }
        }
        public bool LoginCashierToStore(Employee e) {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Employee WHERE UserName=@Username and Password=@Password and IsManager=0", conn);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@Username", e.UserName);
            cmd.Parameters.AddWithValue("@Password", e.UserName);


            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                int rowCount = 0;

                if (reader.HasRows)
                {
                    rowCount = 0;
                    while (reader.Read())
                    {
                        rowCount++;
                    }

                }
                if (rowCount == 1) return true;
                return false;
            }
        }



    }
}
