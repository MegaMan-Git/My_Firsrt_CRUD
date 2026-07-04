using Data_Context.Data;
using ModelAss.IdentityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelAss.Models
{
    public class ApplicationUser_Product
    {
        
        public Guid userId;
        public int productId;

        public ApplicationUser User { get; set; }
        public Product Product { get; set; }
    }
}
