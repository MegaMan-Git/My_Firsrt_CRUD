using Data_Context.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelAss.Models;
using ModelAss.ViewList;

namespace My_Firsrt_CRUD.Controllers
{
    public class AccountController : Controller
    {
        private MyDbContext _context;

        public AccountController(MyDbContext context)
        {
            _context = context;
        }

        public ActionResult SignUpLogin()
        {
            return View();
        }


        // Post: AccountController/Login
        [HttpPost]
        public ActionResult Login(ViewModel_LoginCustomer customer)
        {
            if (ModelState.IsValid)
            {
                //آیا اطلاعات کاربر وجود دارد؟
                var Customer = _context.Customers
                .FirstOrDefault(p => p.Phone == customer.Phone && p.Password == customer.Password);
                //بله
                if(Customer != null)
                {
                    return RedirectToAction("Products", "Product", new {id = Customer.Customer_Id });
                }//خیر
                else
                {
                    return RedirectToAction("SignUpLogin");
                }
            }

            return RedirectToAction("SignUpLogin");
        }


        // POST: AccountController/SignUp
        [HttpPost]
        public ActionResult SignUp(ViewModel_SignCustomer customer)
        {
            if (ModelState.IsValid)
            {
                //کاربر قبلا ثبت نام کرده؟
                var Customer = _context.Customers.FirstOrDefault(p => p.Phone == customer.Phone);
                if(Customer != null)
                {
                    return RedirectToAction("SignUpLogin");
                }
                //ایجاد بدنه
                var NewCustomer = new Customer
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Address = customer.Address,
                    Phone = customer.Phone,
                    Password = customer.Password,
                };

                //ثبت بدنه
                _context.Customers.Add(NewCustomer);
                _context.SaveChanges();

                //ایجاد رابطه اولیه
                var NewRelation = new Customer_Product
                {
                    Customer_id = NewCustomer.Customer_Id,
                    Product_id = 1,
                };
                //ثبت رابطه اولیه
                _context.Customer_Products.Add(NewRelation);
                _context.SaveChanges();

                return RedirectToAction("Products", "Product", new {id = NewCustomer.Customer_Id });
            }
            return BadRequest();
        }

        
    }
}
