using System;
using System.Collections.Generic;
using System.Text;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.DAL.Repository.Interfaces
{
    public interface ICustomerRepos
    {
        List<Customer> GetALLCustomers();
        List<CustomerVehicles> GetCustomerVehicles(int id);
        public void Update(Customer entity);
        public Customer FindCust(int id);
    }
}
