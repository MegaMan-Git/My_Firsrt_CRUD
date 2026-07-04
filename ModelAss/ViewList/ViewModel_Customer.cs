using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelAss.ViewList
{
    public class ViewModel_SignCustomer
    {
        [Required(ErrorMessage ="نام کاربری الزامی هست"),MaxLength(128)]
        public string UserName { get; set; }
        
        [Required(ErrorMessage ="ایمیل الزامی هست"),MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression(@"^09\d{9}$",ErrorMessage ="فرمت تلفن صحیح نیست")]
        public string Phone { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
    public class ViewModel_LoginCustomer
    {
        [Required(ErrorMessage = "نام کاربری الزامی هست"), MaxLength(128)]
        public string UserName { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
