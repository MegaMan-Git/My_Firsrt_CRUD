using Data_Context.Data;
using Microsoft.AspNetCore.Identity;
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
        private readonly IEmailSender _emailSender;
     
        public AccountController
            (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ViewRenderService viewRenderService, IEmailSender emailSender, MyDbContext context)
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
        public async Task<ActionResult> SignUp(ViewModel_SignUpUser model,string returnUrl = null)
        {

            if (returnUrl == null)
                returnUrl = Url.Content("~/Product/Products");

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


            if(Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            
            return RedirectToAction("Products","Product");
        }
        #endregion

        #region عملیات ورود 
        [HttpPost]
        public async Task<ActionResult> Login(ViewModel_LoginUser model,string returnUrl = null)
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
                .PasswordSignInAsync(model.UserName,model.Password,model.RememberMe,false);

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
            }else if (result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "دسترسی نداری");
            }else if (result.RequiresTwoFactor)
            {
                ModelState.AddModelError(string.Empty, "احراز هویت دو مرحله ای مورد نیاز هست");
            }
           

            ModelState.AddModelError(string.Empty, "رمز عبور صحیح نمیباشد");

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

        #region متد دریافت اطلاعات کاربر
        private async Task<ApplicationUser?> GetUser()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                return user;
            }
            return null;
        }
        #endregion

        #region عملیات تایید ایمیل
        public async Task<IActionResult> EmailConfirmed()
        {
            #region دریافت اطلاعات کاربر فعلی و آیا ایمیل از قبل تایید نشده؟
            var user = await GetUser();
            if (user == null || user.EmailConfirmed == true)
            {
                ViewBag.IsEmailSend = false;
                return View();
            }
            #endregion

            //ساخت توکن برای تایید ایمیل
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //انکود کردن توکن
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            //ساخت لینکی که پس از کلیک کاربر روی تایید حساب اجرا میشه
            string? callBackString =
                Url.Action("EmailConfirmation", "Account", new { userId = user.Id.ToString(), token }, Request.Scheme);

            //string به html تزریق لینک تایید ایمیل به صفحه تایید ایمیل و تبدیل 
            //هست string سرویس ارسال ایمیل هم body زیرا 
            string body = await _viewRenderService.RenderToStringAsync
                (ControllerContext, "_RegisterEmail", callBackString);

            //ارسال ایمیل به کاربر
            await _emailSender
                .SendEmailAsync(new EmailModel(user.Email, $"آیا {user.UserName} شما هستید؟", body));

            //جهت نمایش صفحه ارسال موفق
            ViewBag.IsEmailSend = true;
            return View();
        }

        public async Task<ActionResult> EmailConfirmation(string userId ,string token)
        {
            #region دریافت اطلاعات کاربر فعلی و آیا ایمیل از قبل تایید نشده؟
            var USER = await GetUser();
            if (USER == null || USER.EmailConfirmed == true)
            {
                ViewBag.IsEmailSend = false;
                return RedirectToAction("Products", "Product");
            }
            #endregion

            if (token == null || userId == null)
                return BadRequest();

            //اگر کاربر وجود نداشت ادامه نده
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest();

            // دیکود کردن توکن دریافتی جهت استفاده در متد تایید توکن
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            // ارسال توکن و کاربر جهت اعتبارسنجی توکن برای تایید حساب
            var result = await _userManager.ConfirmEmailAsync(user, token);

            return RedirectToAction("Products", "Product");
        }
        #endregion

        #region عملیات فراموشی رمز عبور

        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            ViewBag.IsSent = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPassword_VM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "کاربری با این ایمیل یافت نشد");
                return View(model);
            }
            
            //ساخت توکن تعویض رمز عبور
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //انکود کردن توکن
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            //ساخت لینک ورود به صفحه تعویض رمز برای ایمیل
            string? callBackString = 
                Url.Action("ResetPassword", "Account", new {email = user.Email, token},Request.Scheme);
            //بتواند ایمیل را ارسال کند emailsender برای اینکه  string به html تبدیل صفحه 
            string body = await _viewRenderService
                .RenderToStringAsync(ControllerContext , "_ResetPasswordEmail", callBackString);
            //فراخوانی متد ارسال ایمیل
            await _emailSender.SendEmailAsync(new EmailModel(model.Email,"درخواست تغییر رمز عبور",body));

            
            ViewBag.IsSent = true;
            return View(model);
        }

        //عملیات های مربوط به تغییر رمز پس از کلیک روی لینک موجود در ایمیل
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string email,string token)
        {
            if (email == null || token == null)
                return BadRequest("این صفحه در دسترس نمیباشد");
            var VM = new ResetPassword_VM()
            {
                Email = email,
                Token = token
            };
            return View(VM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPassword_VM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("user not found");
            //دیکود کردن توکن
            model.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Token));
            //تحویل دادن توکن و تعویض رمز عبور کاربر
            var result = await _userManager.ResetPasswordAsync(user,model.Token,model.NewPassword);

            if (result.Succeeded)
                return RedirectToAction("SignUpLogin","Account");

            ModelState.AddModelError(string.Empty, "مشکلی در تغییر رمز پیش آمده");

            return View(model);
        }
        #endregion
    }
}
