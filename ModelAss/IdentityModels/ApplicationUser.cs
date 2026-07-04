using Data_Context.Data;
using Microsoft.AspNetCore.Identity;
using ModelAss.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelAss.IdentityModels
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual ICollection<ApplicationUser_Product> ApplicationUserProducts { get; set; }
    }
}
