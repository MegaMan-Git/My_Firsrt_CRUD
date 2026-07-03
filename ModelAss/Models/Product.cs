using ModelAss.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Context.Data
{
    public class Product
    {
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public int Product_Price { get; set; }
        
        /*
         یک کتگوری میتواند متعلق به چندین محصول باشد و هر محصول فقط یک کتگوری دارد که یعنی 
         اینجا محصول وابسته است
         */
        // کلید خارجی در کلاس وابسته یعنی محصول ساخته شده  
        public int Category_ID;
        public Category category { get; set; }
        public Detail detail { get; set; }
        public ICollection<Customer_Product> Fluent_ProductCustomers { get; set; }


    }
}
