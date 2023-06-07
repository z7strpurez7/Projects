using CustomerDBManager.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDBManager.Repository
{
    class CustomerRepository : ICustomerRepository
    {
       

        //funcitons for extracting data from database
        public List<Customer> GetAllCustomers()
        {
            
            List<Customer> customerList = new List<Customer>();
            //SQL qury for extracting customer data
            string sqlQuery = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer";

            try
            {
                //Statement for connection
                using (SqlConnection connection = new SqlConnection(ConnectionHelper.GetConnectionString()))
                {
                    connection.Open(); //open connection
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            //Walks through all costumers
                            while (reader.Read())
                            {
                                // current Customer
                                Customer current = new Customer();
                                //Reads data by Column name
                                current.CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId"));
                                current.FirstName = !reader.IsDBNull(reader.GetOrdinal("FirstName")) ? reader.GetString(reader.GetOrdinal("FirstName")) : "null";
                                current.LastName = !reader.IsDBNull(reader.GetOrdinal("LastName")) ? reader.GetString(reader.GetOrdinal("LastName")) : "null";
                                current.Country = !reader.IsDBNull(reader.GetOrdinal("Country")) ? reader.GetString(reader.GetOrdinal("Country")) : "null";
                                current.PostalCode = !reader.IsDBNull(reader.GetOrdinal("PostalCode")) ? reader.GetString(reader.GetOrdinal("PostalCode")) : "null";
                                current.Phone = !reader.IsDBNull(reader.GetOrdinal("Phone")) ? reader.GetString(reader.GetOrdinal("Phone")) : "null";
                                current.Email = !reader.IsDBNull(reader.GetOrdinal("Email")) ? reader.GetString(reader.GetOrdinal("Email")) : "null";
                                customerList.Add(current);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Something went wrong with Customer connection");
                throw;
            }
            return customerList;
        }



        public Customer GetCustomer(int Id)
        {
            Customer current = new Customer();
            int searchId = Id;
            //SQL qury for extracting customer data
            string sqlQuery = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer WHERE CustomerId = @searchId;";

            try
            {
                //Statement for connection
                using (SqlConnection connection = new SqlConnection(ConnectionHelper.GetConnectionString()))
                {
                    connection.Open(); //open connection
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        //add parameters
                        command.Parameters.AddWithValue("@SearchId", searchId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //Walks through all costumers
                            while (reader.Read())
                            {
                                //get id by column name
                                current.CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId"));
                                //get stringBy column, but check if null ("GetString" does not accept NULL values)
                                current.FirstName = !reader.IsDBNull(reader.GetOrdinal("FirstName")) ? reader.GetString(reader.GetOrdinal("FirstName")) : "null";
                                current.LastName = !reader.IsDBNull(reader.GetOrdinal("LastName")) ? reader.GetString(reader.GetOrdinal("LastName")) : "null";
                                current.Country = !reader.IsDBNull(reader.GetOrdinal("Country")) ? reader.GetString(reader.GetOrdinal("Country")) : "null";
                                current.PostalCode = !reader.IsDBNull(reader.GetOrdinal("PostalCode")) ? reader.GetString(reader.GetOrdinal("PostalCode")) : "null";
                                current.Phone = !reader.IsDBNull(reader.GetOrdinal("Phone")) ? reader.GetString(reader.GetOrdinal("Phone")) : "null";
                                current.Email = !reader.IsDBNull(reader.GetOrdinal("Email")) ? reader.GetString(reader.GetOrdinal("Email")) : "null";
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Something went wrong with Customer connection");
                throw;
            }
            return current;

        }

        public List<Customer> GetCostumersByName(string nam)
        {
            string name = nam;
            List<Customer> customers = new List<Customer>();

            //SQL qury for extracting customer data
            string sqlQuery = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer WHERE FirstName LIKE @Name OR LastName LIKE @Name";

            try
            {
                //Statement for connection
                using (SqlConnection connection = new SqlConnection(ConnectionHelper.GetConnectionString()))
                {
                    connection.Open(); //open connection
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Name", "%" + name + "%");
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //Walks through all costumers
                            while (reader.Read())
                            {
                                // current Customer
                                Customer current = new Customer();
                                //Reads data by Column name
                                current.CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId"));
                                current.FirstName = !reader.IsDBNull(reader.GetOrdinal("FirstName")) ? reader.GetString(reader.GetOrdinal("FirstName")) : "null";
                                current.LastName = !reader.IsDBNull(reader.GetOrdinal("LastName")) ? reader.GetString(reader.GetOrdinal("LastName")) : "null";
                                current.Country = !reader.IsDBNull(reader.GetOrdinal("Country")) ? reader.GetString(reader.GetOrdinal("Country")) : "null";
                                current.PostalCode = !reader.IsDBNull(reader.GetOrdinal("PostalCode")) ? reader.GetString(reader.GetOrdinal("PostalCode")) : "null";
                                current.Phone = !reader.IsDBNull(reader.GetOrdinal("Phone")) ? reader.GetString(reader.GetOrdinal("Phone")) : "null";
                                current.Email = !reader.IsDBNull(reader.GetOrdinal("Email")) ? reader.GetString(reader.GetOrdinal("Email")) : "null";
                                customers.Add(current);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Something went wrong with Customer connection");
                throw;
            }
            return customers;
        }

        public bool AddCustomer(Customer customer)
        {

            string sqlQuery = @"INSERT INTO Customer (FirstName, LastName, Country, PostalCode, Phone, Email)
                            VALUES (@FirstName, @LastName, @Country, @PostalCode, @Phone, @Email)";
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionHelper.GetConnectionString()))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        command.Parameters.AddWithValue("@LastName", customer.LastName);
                        command.Parameters.AddWithValue("@Country", customer.Country);
                        command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                        command.Parameters.AddWithValue("@Phone", customer.Phone);
                        command.Parameters.AddWithValue("@Email", customer.Email);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Something went wrong with Customer connection");
                throw;
            }
            return true;
        }



        public bool UpdateCustomer(Customer customer)
        {
            
            string sqlQuery = @"UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, Country = @Country,
                            PostalCode = @PostalCode, Phone = @Phone, Email = @Email WHERE CustomerId = @CustomerId";

            try
            {


                using (SqlConnection connection = new SqlConnection(ConnectionHelper.GetConnectionString()))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        command.Parameters.AddWithValue("@LastName", customer.LastName);
                        command.Parameters.AddWithValue("@Country", customer.Country);
                        command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                        command.Parameters.AddWithValue("@Phone", customer.Phone);
                        command.Parameters.AddWithValue("@Email", customer.Email);
                        command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);

                        command.ExecuteNonQuery();
                    }
                }
            } catch (SqlException ex)
            {
                Console.WriteLine("Something went wrong with Customer connection");
                throw;
            }
            return true;
        }
    }
}
