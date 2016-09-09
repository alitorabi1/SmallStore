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



        public void addEmployee(Employee e)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Employee (FirstName, LastName,Phone,CellPhone,Address,PostalCode,BirthDate,JobTitle,HireDate,FireDate,Salary,IsPermenant,IsManager,UserName,Password) VALUES (@FirstName, @LastName,@Phone,@CellPhone,@Address,@PostalCode,@BirthDate,@JobTitle,@HireDate,@FireDate,@Salary,@IsPermenant,@IsManager,@UserName,@Password)"))
            {

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@FirstName", e.FirstName);
                cmd.Parameters.AddWithValue("@LastName", e.LastName);
                cmd.Parameters.AddWithValue("@Phone", e.Phone);
                //   cmd.Parameters.AddWithValue("@CellPhone", e.CellPhone);|
                cmd.Parameters.AddWithValue("@Address", e.Address);
                cmd.Parameters.AddWithValue("@PostalCode", e.PostalCode);
                cmd.Parameters.AddWithValue("@BirthDate", e.BirthDate);
                cmd.Parameters.AddWithValue("@JobTitle", e.JobTitle);
                cmd.Parameters.AddWithValue("@HireDate", e.HireDate);
                cmd.Parameters.AddWithValue("@FireDate", e.FireDate);
                cmd.Parameters.AddWithValue("@Salary", e.Salary);
                cmd.Parameters.AddWithValue("@IsPermenant", e.IsPermenant);
                cmd.Parameters.AddWithValue("@IsManager", e.IsManager);
                cmd.Parameters.AddWithValue("@UserName", e.UserName);
                cmd.Parameters.AddWithValue("@Password", e.Password);

                cmd.ExecuteNonQuery();
            }

        }
        public void addCustomer(Customer c)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Customer (FirstName, LastName,Phone,CellPhone,Address,PostalCode,DiscountPercentage,Reg``isterDate,ModifyDate) VALUES (@FirstName, @LastName,@Phone,@CellPhone,@Address,@PostalCode,@DiscountPercentage,@RegisterDate,@ModifyDate)"))
            {

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@Phone", c.Phone);
                cmd.Parameters.AddWithValue("@CellPhone", c.CellPhone);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@PostalCode", c.PostalCode);
                cmd.Parameters.AddWithValue("@DiscountPercentage", c.DiscountPercentage);
                cmd.Parameters.AddWithValue("@RegisterDate", c.RegisterDate);
                cmd.Parameters.AddWithValue("@ModifyDate", c.ModifyDate);

                cmd.ExecuteNonQuery();
            }

        }
        public bool LoginUser(string user, string pass, out bool isManager)
        {
            SqlCommand cmd = new SqlCommand("SELECT IsManager FROM Employee WHERE UserName=@Username and Password=@Password ", conn);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@Username", user);
            cmd.Parameters.AddWithValue("@Password", pass);

            isManager = false;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                int rowCount = 0;

                if (reader.HasRows)
                {
                    rowCount = 0;
                    while (reader.Read())
                    {
                        rowCount++;
                        isManager = (bool)reader.GetSqlBoolean(reader.GetOrdinal("IsManager"));
                    }

                }
                if (rowCount == 1)
                {

                    return true;
                }
                return false;
            }


        }
        public List<Product> GetAllProductByNameOrBarcode(string str)
        {
            List<Product> productList = new List<Product>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Product where ProductName LIKE '%@ProductName%' OR Barcode  LIKE '%@Barcode%'", conn);

            cmd.Parameters.AddWithValue("@ProductName", str);
            cmd.Parameters.AddWithValue("@Barcode", str);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("Id"));
                        string productName = reader.GetString(reader.GetOrdinal("ProductName"));
                        int categoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));
                        string barcode = reader.GetString(reader.GetOrdinal("Barcode"));
                        int numberInStock = reader.GetInt32(reader.GetOrdinal("NumberInStock"));
                        decimal purchasePrice = reader.GetDecimal(reader.GetOrdinal("PurchasePrice"));
                        decimal salePrice = reader.GetDecimal(reader.GetOrdinal("SalePrice"));
                        string unit = reader.GetString(reader.GetOrdinal("Unit"));

                        Product p = new Product() { Id = id, ProductName = productName, CategoryId = categoryId, Barcode = barcode, NumberInStock = numberInStock, PurchasePrice = purchasePrice, SalePrice = salePrice, Unit = unit };
                        productList.Add(p);

                    }

                }
            }
            return productList;
        }

    }


}

