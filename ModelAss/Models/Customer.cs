using ModelAss.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Context.Data
{
    public class Customer
    {
        public int Customer_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Password {  get; set; }
        public ICollection<Customer_Product> Fluent_ProductCustomers { get; set; }
    }
}
