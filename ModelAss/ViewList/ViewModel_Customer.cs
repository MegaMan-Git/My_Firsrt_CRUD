using System;
using System.Collections.Generic;
using System.Text;

namespace ModelAss.ViewList
{
    public class ViewModel_SignCustomer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
    public class ViewModel_LoginCustomer
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
