using CustomerDBManager.Model;
using CustomerDBManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDBManager
{
    class Program
    {


        static void Main(string[] args)
        {
            //Read all costumers in database; Display: Id, fname, lname, country, postal code, phone and email.
            ICustomerRepository customerRepository = new CustomerRepository();
            List<Customer> customerList = customerRepository.GetAllCustomers();


            //Print costumer by id test
            //Customer customer1 = customerRepository.GetCustomer(13);
            // PrintCustomer(customer1);


            //print all customers test
            // PrintAllCustomers(customerList);

            //List by name test
            //  List<Customer> customerListByName = customerRepository.GetCostumersByName("an");
            //  PrintAllCustomers(customerListByName);


            //Add costumer test
              Customer cTest = new Customer();
              cTest.Address = "frankveien";
              cTest.City = "larvik";
              cTest.Company = "UIA";
              cTest.Country = "Norway";
              cTest.Email = "Testermail.com";
              cTest.State = "Vestfold";
              cTest.FirstName = "Ali";
              cTest.LastName = "tod";
              cTest.PostalCode = "3269";
              cTest.Phone = "9123155";
            Console.WriteLine("Added costumer:" + customerRepository.AddCustomer(cTest));


            
            Customer cTest1 = new Customer();
            cTest1.CustomerId = cTest.CustomerId;
            cTest1.Address = "new frankveien";
            cTest1.City = "new larvik";
            cTest1.Company = "newUIA";
            cTest1.Country = "newNorway";
            cTest1.Email = "newTestermail.com";
            cTest1.State = "newVestfold";
            cTest1.FirstName = "newAli";
            cTest1.LastName = "newtod";
            cTest1.PostalCode = "new3269";
            cTest1.Phone = "9123155";
            Console.WriteLine("Updated:" + customerRepository.UpdateCustomer(cTest1));

            void PrintCustomer(Customer customer)
            {
                Console.WriteLine($"-- {customer.CustomerId}, {customer.FirstName}, {customer.LastName}, {customer.Country}, {customer.PostalCode}, {customer.Email}");
            }

            void PrintAllCustomers(List<Customer> customers)
            {
                foreach (Customer current in customers)
                {
                    Console.WriteLine($"-- {current.CustomerId}, {current.FirstName}, {current.LastName}, {current.Country}, {current.PostalCode}, {current.Email}");
                }
            }


        }
    }
    
}



     
    

