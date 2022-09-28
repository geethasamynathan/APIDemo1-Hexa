using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIDemo1.Models;
using Microsoft.EntityFrameworkCore;
namespace APIDemo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        EASAccountContext _context = null;
        public CustomersController(EASAccountContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }
        [HttpGet("{id}")]
        public Customer GetCustomers(int id)
        {
            Customer customer = _context.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
            return customer;
        }

        [HttpPost]
        public Customer AddNewCustomer(Customer customer)
        {  _context.Customers.Add(customer);
           _context.SaveChanges();
            return customer;
        }
        [HttpPut]
        public Customer UpdateCustomer(Customer customer)
        {
            Customer newCustomer = _context.Customers.Where(c => c.CustomerId == customer.CustomerId).FirstOrDefault();
           // if (newCustomer != null){
                newCustomer.FirstName = customer.FirstName;
                newCustomer.LastName = customer.LastName;
                newCustomer.Contactno = customer.Contactno;
            //  }
            //  _context.Entry<>
            _context.Entry(newCustomer).State = EntityState.Modified;
            _context.SaveChanges();
            return newCustomer;
        }
        [HttpDelete]
        public string DeleteCustomer(int id)
        {
            Customer deleteCustomer = _context.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
            _context.Customers.Remove(deleteCustomer);
            _context.SaveChanges();
            return "details deleted ";
        }
    }
}
