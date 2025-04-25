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
        
        // connect mysql and database
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
            //check if connected
            using (MySqlConnection connectedToDatabase = new MySqlConnection(connectionString))
            {
                // select everything from database
                string query = "SELECT * FROM inventory";

                // excute the query
                using (MySqlCommand command = new MySqlCommand(query, connectedToDatabase))
                {
                    // open the connection of the database
                    connectedToDatabase.Open();

                    // read all the data from the database
                    MySqlDataReader readRecords = command.ExecuteReader();

                    // create data-table. for gridview purposes
                    System.Data.DataTable dataTable = new DataTable();

                    //load all information to datatable
                    dataTable.Load(readRecords);

                    //closed the connection of the database
                    connectedToDatabase.Close();

                    // return value
                    return dataTable;            
                }
            }
        }
        public bool AddProduct(string productName, int productPrice, int productQuantity)
        {
            // insert into inventory (database columns-name) values (@declareNewVariable)

            //insert new data to database
            string query = "insert into inventory (Name, QuantityStock, Price) VALUES (@getProductNameField, @getProductQuantityField, @getProductPriceField)";

            // check if connected
            using (MySqlConnection connectedToDatabase = new MySqlConnection(connectionString))
            {
                // excute the query
                using (MySqlCommand command = new MySqlCommand(query, connectedToDatabase))
                {

                    // pass the production information to database
                    command.Parameters.AddWithValue("@getProductNameField", productName);
                    command.Parameters.AddWithValue("@getProductQuantityField", productQuantity);
                    command.Parameters.AddWithValue("@getProductPriceField", productPrice);

                    // error trapping
                    try
                    {
                        // open database
                        connectedToDatabase.Open();

                        //executes the query
                        command.ExecuteNonQuery();                        

                        // close the databtase
                        connectedToDatabase.Close();

                        //return
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
            // update query with productId
            string query = "UPDATE inventory SET Name=@getNameField, QuantityStock=@getQuantityStockField, Price=@getPriceField WHERE ProductId=@getProductIdField";

            // check database conneciton
            using (MySqlConnection connectedToDatabase = new MySqlConnection(connectionString))
            {
                //// excute the query
                using (MySqlCommand command = new MySqlCommand(query, connectedToDatabase))
                {
                    // pass the new production information to database
                    command.Parameters.AddWithValue("@getProductIdField", id);
                    command.Parameters.AddWithValue("@getNameField", productName);
                    command.Parameters.AddWithValue("@getQuantityStockField", productQuantity);
                    command.Parameters.AddWithValue("@getPriceField", productPrice);

                    // error trapping
                    try
                    {
                        // open database connection
                        connectedToDatabase.Open();

                        // execute query
                        command.ExecuteNonQuery();

                        // close database connection
                        connectedToDatabase.Close();

                        //return
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
            // delete query according to selected productId
            string query = "DELETE FROM inventory WHERE ProductId=" + id;

            // check database connection
            using (MySqlConnection connectedToDatabase = new MySqlConnection(connectionString))
            {

                //execute query
                using (MySqlCommand command = new MySqlCommand(query, connectedToDatabase))
                {                    
                    try
                    {
                        // open DB connection
                        connectedToDatabase.Open();

                        //excute query
                        command.ExecuteNonQuery();

                        // close DB connection
                        connectedToDatabase.Close();

                        //return
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
                // store array from DB
                List<int> pricess = new List<int>();
                List<int> quantity = new List<int>();
                

                // Select price
                string queryPrice = "SELECT Price FROM inventory";

                // open connection
                using (MySqlCommand command = new MySqlCommand(queryPrice, connectedToDatabase))
                {
                    // open database
                    connectedToDatabase.Open();

                    //execute query
                    MySqlDataReader readRecords = command.ExecuteReader();

                    //read all price data 1by1
                    while(readRecords.Read())
                    {
                        //store all price data to list
                        pricess.Add((int) readRecords["Price"]);
                    }                    
                    //close DB connection
                    connectedToDatabase.Close();                    
                }

                //select Quantity
                string queryQuantity = "SELECT QuantityStock FROM inventory";

                //check DB connection
                using (MySqlCommand command = new MySqlCommand(queryQuantity, connectedToDatabase))
                {
                    // open DB 
                    connectedToDatabase.Open();

                    //execute query
                    MySqlDataReader readRecords = command.ExecuteReader();

                    //read all Quantity data 1by1
                    while (readRecords.Read())
                    {
                        //store all Quantity data to list
                        quantity.Add((int)readRecords["QuantityStock"]);
                    }

                    //close DB
                    connectedToDatabase.Close();
                }

                // Computation
                int subTtl = 0;
                int ttl = 0;
                for(int i = 0; i < pricess.Count; i++)
                {
                    subTtl += (quantity[i] * pricess[i]);                    
                }

                // return
                ttl = subTtl;
                return ttl;
            }            
        }
    }
}
