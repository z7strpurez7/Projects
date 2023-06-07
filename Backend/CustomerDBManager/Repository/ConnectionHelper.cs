using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDBManager.Repository
{
    // Connection between application & DB
    class ConnectionHelper
    {
        public static string GetConnectionString()
        {
            // Create the SqlConnectionStringBuilder
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();

            // Set the SQL Server source
            connectionStringBuilder.DataSource = "DESKTOP-ASA9MRI\\SQLEXPRESS";
            // Set the database
            connectionStringBuilder.InitialCatalog = "Chinook";

            // Enable SSL encryption
            connectionStringBuilder.Encrypt = true;

            // Trust the server certificate
            connectionStringBuilder.TrustServerCertificate = true;

            // Use integrated security (Windows authentication)
            connectionStringBuilder.IntegratedSecurity = true;

            // Return the connection string
            return connectionStringBuilder.ConnectionString;
        }
    }
}
