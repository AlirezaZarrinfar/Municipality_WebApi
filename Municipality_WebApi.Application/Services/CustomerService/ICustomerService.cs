using Municipality_WebApi.Common.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipality_WebApi.Application.Services.CustomerService
{
    public interface ICustomerService
    {
        public bool addCustomers(AddCustomersModel model);
        public bool updateCustomers(UpdateCustomersModel model);
        public bool deleteCustomers(long id);
        public string showCustomers();
        public string showCustomersById(long id);
        public bool changeExpireDate(int year, int yearAdd);

    }
}
