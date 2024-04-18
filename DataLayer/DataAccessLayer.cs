using ModernDesign.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Windows;

internal class DataAccessLayer
{
    private readonly string connectionString;

    public DataAccessLayer()
    {
        connectionString = @"Data Source=|DataDirectory|\DataLayer\InventorySystem.db";
    }

    public ObservableCollection<Product> GetProducts()
    {
        ObservableCollection<Product> products = new ObservableCollection<Product>();

        try
        {
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
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving products: {ex.Message}");
        }

        return products;
    }

    public void AddProduct(Product product)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    string query = "INSERT INTO Products (Name, Price, Description, Quantity, Category) VALUES (@Name, @Price, @Description, @Quantity, @Category)";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@Name", product.Name);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@Description", product.Description);
                        command.Parameters.AddWithValue("@Quantity", product.Quantity);
                        command.Parameters.AddWithValue("@Category", product.Category);

                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding product: {ex.Message}");
        }
    }

    public void DeleteProduct(int skuId)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    string query = "DELETE FROM Products WHERE SKU_ID = @SKU_ID";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@SKU_ID", skuId);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting product: {ex.Message}");
        }
    }

    public void UpdateProduct(Product product)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    string query = "UPDATE Products SET Name = @Name, Price = @Price, Description = @Description, Quantity = @Quantity, Category = @Category WHERE SKU_ID = @SKU_ID";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@Name", product.Name);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@Description", product.Description);
                        command.Parameters.AddWithValue("@Quantity", product.Quantity);
                        command.Parameters.AddWithValue("@Category", product.Category);
                        command.Parameters.AddWithValue("@SKU_ID", product.SKU_ID);

                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating product: {ex.Message}");
        }
    }
}
