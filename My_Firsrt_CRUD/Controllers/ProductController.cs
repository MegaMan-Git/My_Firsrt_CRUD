using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ModelAss.Models;
using Data_Context.Data;
using ModelAss.ViewList;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ModelAss.ColorOptions_ForDetail;
using Microsoft.AspNetCore.Identity;
using ModelAss.IdentityModels;
using System.Security.Claims;

namespace My_Firsrt_CRUD.Controllers
{
    public class ProductController : Controller
    {
        #region Dependency Injection
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MyDbContext _context;
        public ProductController(UserManager<ApplicationUser> userManager, MyDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        #endregion

        #region متد دریافت اطلاعات کاربر
        private async Task<ApplicationUser?> GetUser()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user != null)
            {
                return user;     
            }
            return null;
        }
        #endregion

        #region نمایش محصولات
        public async Task<IActionResult> Products()
        {
            #region دریافت اطلاعات کاربر فعلی
            var user = await GetUser();
            if(user == null)
                return NotFound("کاربر پیدا نشد");
            #endregion

            #region آیدی محصولات مربوط به مشتری مدنظر را واکشی میکند
            var Product_IDs = await _context.ApplicationUser_Products
                    .Where(p => p.userId == user.Id).Select(p => p.productId).ToListAsync();
            #endregion

            var MV =  new ViewModel_Product
            {
                // .Include() با استفاده از
                // داده ها بر اساس روابط کلید خارجی واکشی و به شیء اصلی متصل (جایگذاری) میشوند
                // بنابراین کتگوری مرتبط به محصول واکشی میشود
                Products = await _context.Products.Include(p => p.category)
                //فقط محصولات مربوط به مشتری مدنظر را واکشی میکند
                   .Where(p => Product_IDs.Contains(p.Product_Id)).ToListAsync(),

                // جهت نمایش و تغییر دسته بندی ها در صفحه نمایش محصولات
                Categories = await _context.Categories.ToListAsync()
              
            };


            if (!MV.Products.Any())
            {
                return NotFound();
            }

            //آیا حساب تایید شده؟
            ViewBag.IsEmailConfirmed = user.EmailConfirmed;

            return View(MV);
        }
        #endregion

        #region ایجاد محصول جدید
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ViewModel_CreateProduct model)
        {
            if (ModelState.IsValid)
            {
                #region دریافت اطلاعات کاربر فعلی
            var user = await GetUser();
            if(user == null)
                return NotFound("کاربر پیدا نشد");
            #endregion


                #region افزودن ابتدا محصول سپس جزئیات آن
                var NewProduct = new Product
                {
                    Product_Name = model.Product_Name,
                    Product_Price = model.Product_Price,
                    Category_ID = model.Category_ID
                };

                await _context.Products.AddAsync(NewProduct);
                await _context.SaveChangesAsync();

                var NewDetail = new Detail
                {
                    Detail_Id = NewProduct.Product_Id,
                    In_Stock = model.In_Stock,
                    Detail_Description = model.Detail_Description,
                    Detail_Color = model.Detail_Color,
                    RegistrationDate = model.RegistrationDate
                };

                await _context.Details.AddAsync(NewDetail);
                await _context.SaveChangesAsync();
                #endregion


                var NewRelation = new ApplicationUser_Product
                {
                    productId = NewProduct.Product_Id,
                    userId = user.Id
                };
                await _context.ApplicationUser_Products.AddAsync(NewRelation);
                await _context.SaveChangesAsync();

                return RedirectToAction("Products");
            }

            return RedirectToAction("Products");
        }

        #endregion

        #region حذف محصول
        public async Task<IActionResult> ProductDelete(int? itemid)
        {
            if (itemid != null)
            {
                var Product = await _context.Products
                    .Include(p => p.detail)
                    .FirstOrDefaultAsync(p => p.Product_Id == itemid);
                if (Product != null)
                {
                    _context.Details.Remove(Product.detail);
                    _context.Products.Remove(Product);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Products");
            }
            return NotFound("محصولی یافت نشد");
        }
        #endregion
        
        #region تغییر جزئیات محصول
        public async Task<IActionResult> DetailEdit(int? itemid)
        {
            #region دریافت اطلاعات کاربر فعلی
            var user = await GetUser();
            if (user == null)
                return NotFound("کاربر پیدا نشد");
            #endregion


            if (itemid != null)
            {
                var detail = await _context.Details.FirstOrDefaultAsync(p => p.Detail_Id == itemid);

                if (detail == null)
                {
                    return RedirectToAction("Error");
                }
                //ارسال جزئیات
                ViewBag.Detail = detail;
                //آیا حساب تایید شده؟
                ViewBag.IsEmailConfirmed = user.EmailConfirmed;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DetailEdit(ViewModelDetail detail)
        {
            var EditDetail = await _context.Details.FirstOrDefaultAsync(p => p.Detail_Id == detail.Detail_Id);
            if (EditDetail == null)
            {
                return BadRequest();
            }

            EditDetail.In_Stock = detail.In_Stock;
            EditDetail.Detail_Color = detail.Detail_Color;
            EditDetail.Detail_Description = detail.Detail_Description.Trim();
            //اگر کاربر تاریخ را عوض نکرد تاریخ قبل را نشان بده
            if (detail.RegistrationDate != null)
            {
                EditDetail.RegistrationDate = detail.RegistrationDate;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("DetailEdit", new { itemid = detail.Detail_Id });
        }

        #endregion

        #region نمایش دسته بندی ها

        public async Task<IActionResult> Categories()
        {
            #region دریافت اطلاعات کاربر فعلی
            var user = await GetUser();
            if (user == null)
                return NotFound("کاربر پیدا نشد");
            #endregion

            var VM = new ViewModel_Product
            {
                Categories = await _context.Categories.ToListAsync()               
            };

            //آیا حساب تایید شده؟
            ViewBag.IsEmailConfirmed = user.EmailConfirmed;

            return View(VM);
        }
        #endregion

        #region ایجاد دسته بندی
        [HttpPost]
        public async Task<IActionResult> CategoryCreate(string? Category_NewName)
        {
            if (Category_NewName != null)
            {
                var NewCategory = new Category
                {
                    Category_Name = Category_NewName
                };
                await _context.Categories.AddAsync(NewCategory);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Categories");
        }
        #endregion
        
        #region  بروزرسانی دسته بندی محصولات
        [HttpPost]
        //تغییر دسته بندی از بین گزینه های موجود
        public async Task<IActionResult> CategoryUpdate(ProductListUpdateModel model)
        {
            if (model?.Products == null || !model.Products.Any())
                return RedirectToAction("Products");

            var categoryMap = model.Products
                .ToDictionary(p => p.Product_Id, p => p.Category_Id);

            var productIds = categoryMap.Keys.ToList();

            var products = await _context.Products
                .Where(p => productIds.Contains(p.Product_Id))
                .ToListAsync();

            foreach (var product in products)
            {
                product.Category_ID = categoryMap[product.Product_Id];
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Products");

            //if (model != null)
            //{
            //    foreach (var item in model.Products)
            //    {
            //        var Temp = await _context.Products.FirstOrDefaultAsync(p => p.Product_Id == item.Product_Id);
            //        if (Temp != null)
            //        {
            //            Temp.Category_ID = item.Category_Id;
            //        }
            //    }
            //    await _context.SaveChangesAsync();
            //}

            //return RedirectToAction("Products");
        }
        //تغییرنام دسته بندی های موجود
        public async Task<IActionResult> CategoryNameUpdate(ListCategories model)
        {
            if (model != null)
            {
                var IDs = model.Categories.Select(p => p.Category_Id).ToList();

                var DB_Categories = await _context.Categories.Where(p => IDs.Contains(p.Category_Id)).ToListAsync();

                foreach (var item in model.Categories)
                {
                    var Temp = DB_Categories.FirstOrDefault(p => p.Category_Id == item.Category_Id);
                    if (Temp != null)
                    {
                        Temp.Category_Name = item.Category_NewName;
                    }
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Categories");
        }
        #endregion 
        
        #region حذف دسته بندی
        public async Task<IActionResult> CategoryDelete(int CategoryID)
        {
            #region تغییر دسته بندی محصولاتی که قرار است دسته بندی آنان حذف شود
            var ProductsCategory_ID = await _context.Products
                .Where(p => p.Category_ID == CategoryID).ToListAsync();
            if (ProductsCategory_ID != null)
            {
                foreach (var items in ProductsCategory_ID)
                {
                    items.Category_ID = 1;
                }
                await _context.SaveChangesAsync();
            }
            #endregion

            var item = await _context.Categories.FindAsync(CategoryID);
            if (item != null)
            {
                _context.Categories.Remove(item);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Categories");
        }
        #endregion

        #region Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("/Error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
