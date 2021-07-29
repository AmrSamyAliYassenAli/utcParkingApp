using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UTCAPPCMS.DAL.DBContext;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces;

namespace UTCAPPCMS.DAL.Repository.Repos
{
    public class CustomerRepository: ICustomerRepos
    {
        private readonly UTCAPPCMS_DBContext context;

        public CustomerRepository(UTCAPPCMS_DBContext _Context)
        {
            context = _Context;
        }

        public Customer FindCust(int id)
        {
            return context.Customer.Find(id);
        }

        public List<Customer> GetALLCustomers()
        {
            return context.Customer.Include(c=>c.SubscriptionDuration).Include(c=>c.SubscriptionDuration.Subscription).ToList();
        }

        public List<CustomerVehicles> GetCustomerVehicles(int id)
        {
            return context.CustomerVehicles.Where(cv => cv.CustomerId == id).ToList();
        }


        public void Update( Customer entity)
        {
            context.Customer.Update(entity);
            context.SaveChanges();
        }


    }
}
//.Subscription
//.Where(x => x.IsDeleted == false && x.IsEnable == true)