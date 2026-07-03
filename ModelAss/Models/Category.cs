using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Context.Data
{
    public class Category
    {
        public int Category_Id {  get; set; }
        public string Category_Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
