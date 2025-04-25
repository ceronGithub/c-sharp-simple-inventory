using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invetory.classes
{
    class InventoryManager
    {
        /*
         * 1. arrange the design
         * 2. check mysql if it has connection and ready to make database
         * 3. connect mysql to c# VS
         * 3.1. right-click project name (top-right-side)
         * 3.2. manage nuget
         * 3.3. search Mysql.data
         * 3.4. install mysql.data
         * 4. create variable name for connectionString
         * 5. free todo anything.
         */        
        string connectionString = "server=localhost;uid=root;pwd=password;database=inventroy";
        public bool checkDatabaseIfConnected()
        {
            MySqlConnection sqlConnection = new MySqlConnection();
            sqlConnection.ConnectionString = connectionString;
            try
            {
                sqlConnection.Open();
                return true;
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                return false;            
            }
        }

        public DataTable getData()
        {
            using (MySqlConnection connectedToDatabase = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM inventory";
                using (MySqlCommand command = new MySqlCommand(query, connectedToDatabase))
                {
                    connectedToDatabase.Open();
                    MySqlDataReader readRecords = command.ExecuteReader();
                    System.Data.DataTable dataTable = new DataTable();
                    dataTable.Load(readRecords);
                    connectedToDatabase.Close();
                    return dataTable;            
                }
            }
        }
        public bool AddProduct(string productName, int productPrice, int productQuantity)
        {
            // insert into inventory (database columns-name) values (@declareNewVariable)
            string query = "insert into inventory (Name, QuantityStock, Price) VALUES (@getProductNameField, @getProductQuantityField, @getProductPriceField)";
            using (MySqlConnection connectedToDatabase = new MySqlConnection(connectionString))
            {                
                using (MySqlCommand command = new MySqlCommand(query, connectedToDatabase))
                {
                    command.Parameters.AddWithValue("@getProductNameField", productName);
                    command.Parameters.AddWithValue("@getProductQuantityField", productQuantity);
                    command.Parameters.AddWithValue("@getProductPriceField", productPrice);
                    try
                    {
                        connectedToDatabase.Open();
                        command.ExecuteNonQuery();                        
                        connectedToDatabase.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // if fail : return false
                        return false;
                    }
                                       
                }
            }
        }

        public bool UpdateProduct(string productName, int productPrice, int productQuantity, int id)
        {
            string query = "UPDATE inventory SET Name=@getNameField, QuantityStock=@getQuantityStockField, Price=@getPriceField WHERE ProductId=@getProductIdField";
            using (MySqlConnection connectedToDatabase = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connectedToDatabase))
                {
                    command.Parameters.AddWithValue("@getProductIdField", id);
                    command.Parameters.AddWithValue("@getNameField", productName);
                    command.Parameters.AddWithValue("@getQuantityStockField", productQuantity);
                    command.Parameters.AddWithValue("@getPriceField", productPrice);
                    try
                    {
                        connectedToDatabase.Open();
                        command.ExecuteNonQuery();
                        connectedToDatabase.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // if fail : return false
                        return false;
                    }

                }
            }
        }

        public bool RemoveProduct(int id)
        {
            string query = "DELETE FROM inventory WHERE ProductId=" + id;
            using (MySqlConnection connectedToDatabase = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connectedToDatabase))
                {                    
                    try
                    {
                        connectedToDatabase.Open();
                        command.ExecuteNonQuery();
                        connectedToDatabase.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // if fail : return false
                        return false;
                    }

                }
            }
        }

        public int GetTotalValue()
        {
            using (MySqlConnection connectedToDatabase = new MySqlConnection(connectionString))
            {
                List<int> pricess = new List<int>();
                List<int> quantity = new List<int>();
                
                string queryPrice = "SELECT Price FROM inventory";
                using (MySqlCommand command = new MySqlCommand(queryPrice, connectedToDatabase))
                {
                    connectedToDatabase.Open();
                    MySqlDataReader readRecords = command.ExecuteReader();
                    while(readRecords.Read())
                    {
                        pricess.Add((int) readRecords["Price"]);
                    }                    
                    connectedToDatabase.Close();                    
                }
                string queryQuantity = "SELECT QuantityStock FROM inventory";
                using (MySqlCommand command = new MySqlCommand(queryQuantity, connectedToDatabase))
                {
                    connectedToDatabase.Open();
                    MySqlDataReader readRecords = command.ExecuteReader();
                    while (readRecords.Read())
                    {
                        quantity.Add((int)readRecords["QuantityStock"]);
                    }
                    connectedToDatabase.Close();
                }

                int subTtl = 0;
                int ttl = 0;
                for(int i = 0; i < pricess.Count; i++)
                {
                    subTtl += (quantity[i] * pricess[i]);                    
                }
                ttl = subTtl;
                return ttl;
            }            
        }
    }
}
