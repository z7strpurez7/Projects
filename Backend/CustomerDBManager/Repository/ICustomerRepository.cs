using CustomerDBManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDBManager.Repository
{
    public interface ICustomerRepository
    {
        List<Customer> GetAllCustomers();  ///Read all costumers in database
        Customer GetCustomer(int id); ///Read specific costumer from database by Id

        List<Customer> GetCostumersByName(string nam);

        bool AddCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
      

    }
}
