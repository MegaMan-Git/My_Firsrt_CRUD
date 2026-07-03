using Data_Context.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelAss.Models
{
    public class Customer_Product
    {
        public int Product_id;
        public int Customer_id;
        
        public Customer customer { get; set; }
        public Product product { get; set; }
        
    }
}
