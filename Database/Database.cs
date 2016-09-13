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
        //const string CONN_STRING = @"Data Source=ipd8.database.windows.net;Initial Catalog=store;Integrated Security=False;User ID=ipd8abbott;Password=Abbott2000;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        const string CONN_STRING = @"Data Source=ali-ipd8.database.windows.net;Initial Catalog=store;Integrated Security=False;User ID=ali-ipd8;Password=torabi-2016;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SqlConnection conn;
        public Database()
        {
            conn = new SqlConnection(CONN_STRING);
            conn.Open();//it may throw exception
        }

        //  ******************* CRUD methods for Employee ************************ -->

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

        // <-- ******************* CRUD methods for Employee ************************

        //  ********************** CRUD methods for Customer ************************ -->

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

        // <--  ********************** CRUD methods for Customer ************************

        //  ******************* CRUD methods for Product ************************ -->

        public List<Product> GetAllProductByNameOrBarcode(string str)
        {
            List<Product> productList = new List<Product>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Product where ProductName LIKE @ProductName OR Barcode  LIKE @Barcode", conn);

            cmd.Parameters.AddWithValue("@ProductName", "%" + str + "%");
            cmd.Parameters.AddWithValue("@Barcode", "%" + str + "%");
            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("ProductId"));
                        string productName = reader.GetString(reader.GetOrdinal("ProductName"));
                        int categoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));
                        string barcode = reader.GetString(reader.GetOrdinal("Barcode"));
                        int numberInStock = reader.GetInt32(reader.GetOrdinal("NumberInStock"));
                        decimal purchasePrice = reader.GetDecimal(reader.GetOrdinal("PurchasePrice"));
                        decimal salePrice = reader.GetDecimal(reader.GetOrdinal("SalePrice"));
                        string unit = reader.GetString(reader.GetOrdinal("Unit"));
                        decimal specialDiscount = reader.GetDecimal(reader.GetOrdinal("SpecialDiscount"));

                        Product p = new Product() { Id = id, ProductName = productName, CategoryId = categoryId, Barcode = barcode, NumberInStock = numberInStock, PurchasePrice = purchasePrice, SalePrice = salePrice, Unit = unit, SpecialDiscount = specialDiscount };
                        productList.Add(p);

                    }

                }
            }
            return productList;
        }

        public void AddProductItem(Product p)
        {
            //            SqlCommand cmd = new SqlCommand("INSERT INTO Product (ProductName, CategoryId, Barcode, NumberInStock, PurchasePrice, SalePrice, Unit, ProductImage, SpecialDiscount) VALUES (@ProductName, @CategoryId, @Barcode, @NumberInStock, @PurchasePrice, @SalePrice, @Unit, @ProductImage, @SpecialDiscount)", conn);
            SqlCommand cmd = new SqlCommand("INSERT INTO Product (ProductName, CategoryId, Barcode, NumberInStock, PurchasePrice, SalePrice, Unit, SpecialDiscount) VALUES (@ProductName, @CategoryId, @Barcode, @NumberInStock, @PurchasePrice, @SalePrice, @Unit, @SpecialDiscount)", conn);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@ProductName", p.ProductName);
            cmd.Parameters.AddWithValue("@CategoryId", p.CategoryId);
            cmd.Parameters.AddWithValue("@Barcode", p.Barcode);
            cmd.Parameters.AddWithValue("@NumberInStock", p.NumberInStock);
            cmd.Parameters.AddWithValue("@PurchasePrice", p.PurchasePrice);
            cmd.Parameters.AddWithValue("@SalePrice", p.SalePrice);
            cmd.Parameters.AddWithValue("@Unit", p.Unit);
            //            cmd.Parameters.AddWithValue("@ProductImage", p.ProductImage);
            cmd.Parameters.AddWithValue("@SpecialDiscount", p.SpecialDiscount);

            cmd.ExecuteNonQuery();
        }

        public List<Product> GetAllProducts()
        {
            List<Product> ProductList = new List<Product>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Product", conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("ProductId"));
                            string productName = reader.GetString(reader.GetOrdinal("ProductName"));
                            int categoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"));
                            string barcode = reader.GetString(reader.GetOrdinal("Barcode"));
                            int numberInStock = reader.GetInt32(reader.GetOrdinal("NumberInStock"));
                            decimal purchasePrice = reader.GetDecimal(reader.GetOrdinal("PurchasePrice"));
                            decimal salePrice = reader.GetDecimal(reader.GetOrdinal("SalePrice"));
                            string unit = reader.GetString(reader.GetOrdinal("Unit"));
                            //                            string productImage = reader.GetString(reader.GetOrdinal("ProductImage"));
                            decimal specialDiscount = reader.GetDecimal(reader.GetOrdinal("SpecialDiscount"));


                            //                            Product p = new Product() { ProductName = productName, CategoryId = categoryId, Barcode = barcode, NumberInStock = numberInStock, PurchasePrice = purchasePrice, SalePrice = salePrice, Unit = unit, ProductImage = productImage, SpecialDiscount = specialDiscount };
                            Product p = new Product() { Id = id, ProductName = productName, CategoryId = categoryId, Barcode = barcode, NumberInStock = numberInStock, PurchasePrice = purchasePrice, SalePrice = salePrice, Unit = unit, SpecialDiscount = specialDiscount };
                            ProductList.Add(p);

                        }
                    }
                }
            }
            return ProductList;
        }

        public void DeleteProductItemById(int Id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Product WHERE ProductId = " + Id, conn);
            cmd.ExecuteNonQuery();
        }

        public void UpdateProductItem(Product p)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Product SET ProductName = productName, CategoryId = @CategoryId, Barcode = @Barcode, NumberInStock = @NumberInStock, PurchasePrice = @PurchasePrice, SalePrice = @SalePrice, Unit = @Unit, SpecialDiscount = @SpecialDiscount WHERE (ProductId = " + p.Id + ")", conn);
            //             + "Unit = @Unit, ProductImage = @ProductImage, SalePrice = @SalePrice, SpecialDiscount = @SpecialDiscount WHERE (ProductId = " + p.Id + ")", conn);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@ProductName", p.ProductName);
            cmd.Parameters.AddWithValue("@CategoryId", p.CategoryId);
            cmd.Parameters.AddWithValue("@Barcode", p.Barcode);
            cmd.Parameters.AddWithValue("@NumberInStock", p.NumberInStock);
            cmd.Parameters.AddWithValue("@PurchasePrice", p.PurchasePrice);
            cmd.Parameters.AddWithValue("@SalePrice", p.SalePrice);
            cmd.Parameters.AddWithValue("@Unit", p.Unit);
            //            cmd.Parameters.AddWithValue("@ProductImage", p.ProductImage);
            cmd.Parameters.AddWithValue("@SpecialDiscount", p.SpecialDiscount);

            cmd.ExecuteNonQuery();
        }


        // <--  ********************** CRUD methods for Product ************************

        //  ************************** CRUD methods for ProductCategory ************************ -->

        public void AddCategoryItem(ProductCategory pc)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO ProductCategory (Category, Description) VALUES (@Category, @Description)", conn);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@Category", pc.Category);
            cmd.Parameters.AddWithValue("@Description", pc.Description);
            cmd.ExecuteNonQuery();
        }

        public List<ProductCategory> GetAllCategories()
        {
            List<ProductCategory> CategoryList = new List<ProductCategory>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductCategory", conn))
            {

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("CategoryId"));
                            string category = reader.GetString(reader.GetOrdinal("Category"));
                            string description = reader.GetString(reader.GetOrdinal("Description"));

                            ProductCategory pc = new ProductCategory() { CategoryId = id, Category = category, Description = description };
                            CategoryList.Add(pc);

                        }
                    }
                }
            }
            return CategoryList;
        }

        public void DeleteCategoryItemsById(int Id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM ProductCategory WHERE CategoryId = " + Id, conn);
            cmd.ExecuteNonQuery();
        }

        public void UpdateCategoryItems(ProductCategory pc)
        {
            SqlCommand cmd = new SqlCommand("UPDATE ProductCategory SET Category = @Category, Description=@Description WHERE (CategoryId = " + pc.CategoryId + ")", conn);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@Category", pc.Category);
            cmd.Parameters.AddWithValue("@Description", pc.Description);
            cmd.ExecuteNonQuery();
        }

        // <-- ******************* CRUD methods for ProductCategory ************************

    }
}

