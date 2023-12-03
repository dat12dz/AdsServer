using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsServer
{

    internal class SqlConnectionHandler
    {
        SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
        public SqlConnectionHandler()
        {    
            connectionString.DataSource = System.Net.Dns.GetHostName();
            connectionString.InitialCatalog = "AdsServer";
            connectionString.IntegratedSecurity = true;
            connectionString.TrustServerCertificate = true;
            Connect();
        }   
        
        SqlConnection connection;
        void Connect()
        {
            try
            {
                connection = new SqlConnection(connectionString.ToString());
                // Open the connection
                connection.Open();

                // Your SQL query


            }
            catch (Exception ex)
            {
                ex.PrintExceptionInfo();
            }
        }
        public void Query(string query)
        {
            try
            {

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute the query
                    SqlDataReader reader = command.ExecuteReader();

                    // Process the results
                    while (reader.Read())
                    {
   
                        // Access data using reader
                        // For example: int id = reader.GetInt32(0);
                        //             string name = reader.GetString(1);
                        //             Console.WriteLine($"ID: {id}, Name: {name}");
                    }
                }
            }
            catch (Exception e)
            {
                e.PrintExceptionInfo();
            }
            // Create a SqlCommand object
        }
        public void NonQuery(string  query)
        {
            try
            {

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute the query
                    int reader = command.ExecuteNonQuery();             
                }
            }
            catch (Exception E) 
            {
                E.PrintExceptionInfo();
            }
        }
    }
}
