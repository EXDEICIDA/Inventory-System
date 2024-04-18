using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using ModernDesign.MVVM.Model;

namespace ModernDesign.MVVM.ViewModel
{
    internal class DataAccessLayer
    {
        private readonly string connectionString;

        public DataAccessLayer()
        {
            // Set the connection string to your SQLite database file
            connectionString = @"Data Source=|DataDirectory|\DataLayer\InventorySystem.db";
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Products";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                SKU_ID = Convert.ToInt32(reader["SKU_ID"]),
                                Name = Convert.ToString(reader["Name"]),
                                Price = Convert.ToDecimal(reader["Price"]),
                                Description = Convert.ToString(reader["Description"]),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Category = Convert.ToString(reader["Category"])
                            };
                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }

        public void AddProduct(Product product)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Products (SKU_ID, Name, Price, Description, Quantity, Category) VALUES (@SKU_ID, @Name, @Price, @Description, @Quantity, @Category)";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SKU_ID", product.SKU_ID);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Quantity", product.Quantity);
                    command.Parameters.AddWithValue("@Category", product.Category);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduct(Product product)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Products WHERE SKU_ID = @SKU_ID";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SKU_ID", product.SKU_ID);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProduct(Product product)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE Products SET Name = @Name, Price = @Price, Description = @Description, Quantity = @Quantity, Category = @Category WHERE SKU_ID = @SKU_ID";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Quantity", product.Quantity);
                    command.Parameters.AddWithValue("@Category", product.Category);
                    command.Parameters.AddWithValue("@SKU_ID", product.SKU_ID);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
