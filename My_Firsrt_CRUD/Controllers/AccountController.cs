using Data_Context.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using ModelAss.IdentityModels;
using ModelAss.Models;
using ModelAss.ViewList;
using My_Firsrt_CRUD.Tools;
using System.Text;

namespace My_Firsrt_CRUD.Controllers
{
    public class AccountController : Controller
    {
        #region Dependency Injection
        private readonly MyDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ViewRenderService _viewRenderService;
        private readonly EmailSender _emailSender;
     
        public AccountController
            (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ViewRenderService viewRenderService, EmailSender emailSender, MyDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _emailSender = emailSender;
        }
        #endregion

        #region  صفحه ثبت نام و لاگین
        public async Task<ActionResult> SignUpLogin(string returnUrl = null)
        {
            if(returnUrl == null)
                returnUrl = Url.Content("~/Product/Products");

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        #endregion

        #region عملیات ثبت نام 
        [HttpPost]
        public async Task<ActionResult> SignUp(ViewModel_SignUpUser model)
        {

            if(!ModelState.IsValid)
                return View("SignUpLogin");


            var result = await _userManager.CreateAsync(new ApplicationUser()
            {
                UserName = model.UserName,
                NormalizedUserName = model.UserName.ToUpper(),
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper(),
                
            },model.Password);
        
            if(!result.Succeeded)
            {
                foreach(var error  in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }

                return View("SignUpLogin");
            }

            //پیدا کردن کاربر تازه ثبت نام شده
            var user = await _userManager.FindByNameAsync(model.UserName);

            //ایجاد رابطه اولیه کاربر با محصولات
            await _context.ApplicationUser_Products.AddAsync(new ApplicationUser_Product()
            {
                userId = user.Id,
                productId = 1 //محصول مخفی
            });
            await _context.SaveChangesAsync();
            //لاگین کردن
            await _signInManager.SignInAsync(user,false);

            return  RedirectToAction("Products","Product");
        }
        #endregion

        #region عملیات ورود 
        [HttpPost]
        public async Task<ActionResult> Login(ViewModel_LoginUser model, string returnUrl = null)
        {
            //اگر خالی بود آدرس صفحه اصلی رو بده
            if (returnUrl == null)
                returnUrl = Url.Content("~/Product/Products");

            if (!ModelState.IsValid)
            {
                return View("SignUpLogin", model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            if(user == null)
            {
                ModelState.AddModelError(string.Empty, "نام کاربری یا رمز صحیح نمیباشد");

                return View("SignUpLogin",model);
            }

            var result = await _signInManager
                .PasswordSignInAsync(user,model.Password,model.RememberMe,false);

            if (result.Succeeded)
            {
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Products", "Product");
            }else if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "حساب کاربری به مدت چند دقیقه قفل شده");

                return View("SignUpLogin", model);
            }

            ModelState.AddModelError(string.Empty, "مشکلی در احرازهویت شما پیش آمده");

            return View("SignUpLogin", model);
        }
        #endregion

        #region عملیات خروج
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignUpLogin");
        }

        #endregion
        //public async Task<ActionResult> EmailConfirmation(string token, string email)
        //{
        //    if (token == null || email == null) 
        //        return BadRequest();

        //    var user = await _userManager.FindByEmailAsync(email);
        //    if(user == null) 
        //        return BadRequest();

        //}

        //ساخت توکن برای تایید ایمیل
        //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        ////انکود کردن توکن
        //token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        //    //ساخت لینکی که پس از کلیک کاربر روی تایید حساب اجرا میشه
        //    string? callBackString =
        //        Url.Action("EmailConfirmation", "Account", new { userId = user.Id, token }, Request.Scheme);

        ////string به html تزریق لینک تایید ایمیل به صفحه تایید ایمیل و تبدیل 
        ////هست string سرویس ارسال ایمیل هم body زیرا 
        //string body = await _viewRenderService.RenderToStringAsync
        //    (ControllerContext, "_RegisterEmail", callBackString);

        ////ارسال ایمیل به کاربر
        //await _emailSender
        //    .SendEmailAsync(new EmailModel(user.Email, $"آیا {user.UserName} شما هستید؟", body));


    }
}
