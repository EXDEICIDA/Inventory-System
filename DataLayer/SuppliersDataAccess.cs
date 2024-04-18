using ModernDesign.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;

namespace DataLayer
{
    public class SuppliersDataAccess
    {
        private readonly string connectionString = @"Data Source=|DataDirectory|\DataLayer\InventorySystem.db";

        // Method to retrieve all suppliers from the database
        public ObservableCollection<Supplier> GetAllSuppliers()
        {
            ObservableCollection<Supplier> suppliers = new ObservableCollection<Supplier>();

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Suppliers";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                suppliers.Add(new Supplier
                                {
                                    SupplierID = Convert.ToInt32(reader["SupplierID"]),
                                    Name = reader["Name"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    ContactInfo = reader["ContactInfo"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving suppliers: {ex.Message}");
            }

            return suppliers;
        }

        // Method to add a new supplier to the database
        public void AddSupplier(Supplier supplier)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Suppliers (Name, Address, ContactInfo) VALUES (@Name, @Address, @ContactInfo)";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", supplier.Name);
                        command.Parameters.AddWithValue("@Address", supplier.Address);
                        command.Parameters.AddWithValue("@ContactInfo", supplier.ContactInfo);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding supplier: {ex.Message}");
            }
        }

        // Method to delete a supplier from the database
        public void DeleteSupplier(int supplierId)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Suppliers WHERE SupplierID = @SupplierID";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SupplierID", supplierId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting supplier: {ex.Message}");
            }
        }

        // Method to update a supplier's details in the database
        public void UpdateSupplier(Supplier supplier)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Suppliers SET Name = @Name, Address = @Address, ContactInfo = @ContactInfo WHERE SupplierID = @SupplierID";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", supplier.Name);
                        command.Parameters.AddWithValue("@Address", supplier.Address);
                        command.Parameters.AddWithValue("@ContactInfo", supplier.ContactInfo);
                        command.Parameters.AddWithValue("@SupplierID", supplier.SupplierID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating supplier: {ex.Message}");
            }
        }
    }
}
