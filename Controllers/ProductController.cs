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

namespace My_Firsrt_CRUD.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly MyDbContext _context;
        public ProductController(ILogger<ProductController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        #region نمایش محصولات
        public IActionResult Products(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //فقط ایدی محصولات مربوط به مشتری مدنظر را واکشی میکند
            var Product_IDs = _context.Customer_Products
                    .Where(p => p.Customer_id == id).Select(p => p.Product_id);

            var MV = new ViewModel_Product
            {
                // .Include() با استفاده از
                // داده‌ ها بر اساس روابط کلید خارجی واکشی و به شیء اصلی متصل (جایگذاری) می‌شوند
                // بنابراین کتگوری مرتبط به محصول واکشی میشود
                Products = _context.Products.Include(p => p.category)
                //فقط محصولات مربوط به مشتری مدنظر را واکشی میکند
                   .Where(p => Product_IDs.Contains(p.Product_Id)).ToList(),

                // جهت نمایش و تغییر دسته بندی ها در صفحه نمایش محصولات
                Categories = _context.Categories.ToList(),

                //انتقال آیدی
                Customer_ID = (int)id
            };


            if (!MV.Products.Any())
            {
                return NotFound();
            }
            return View(MV);
        }
        #endregion
        
        #region ایجاد محصول جدید
        [HttpPost]

        public IActionResult ProductCreate(ViewModel_CreateProduct model)
        {
            if (model != null)
            {

                #region افزودن ابتدا محصول سپس جزئیات آن
                var NewProduct = new Product
                {
                    Product_Name = model.Product_Name,
                    Product_Price = model.Product_Price,
                    Category_ID = model.Category_ID
                };

                _context.Products.Add(NewProduct);
                _context.SaveChanges();

                var NewDetail = new Detail
                {
                    Detail_Id = NewProduct.Product_Id,
                    In_Stock = model.In_Stock,
                    Detail_Description = model.Detail_Description,
                    Detail_Color = model.Detail_Color,
                    RegistrationDate = model.RegistrationDate
                };

                _context.Details.Add(NewDetail);
                _context.SaveChanges();
                #endregion


                var NewRelation = new Customer_Product
                {
                    Product_id = NewProduct.Product_Id,
                    Customer_id = model.Customer_ID
                };
                _context.Customer_Products.Add(NewRelation);
                _context.SaveChanges();

                return RedirectToAction("Products", new { id = model.Customer_ID });
            }

            return NotFound();
        }

        #endregion
        
        #region حذف محصول
        public IActionResult ProductDelete(int? itemid, int id)
        {
            if (itemid != null)
            {
                var Product = _context.Products.Include(p => p.detail).FirstOrDefault(p => p.Product_Id == itemid);
                if (Product != null)
                {
                    _context.Details.Remove(Product.detail);
                    _context.Products.Remove(Product);
                }
                _context.SaveChanges();
                return RedirectToAction("Products", id);
            }
            return NotFound();
        }
        #endregion
        
        #region تغییر جزئیات محصول
        public IActionResult DetailEdit(int? itemid, int id)
        {
            if (itemid != null)
            {
                var detail = _context.Details.FirstOrDefault(p => p.Detail_Id == itemid);

                if (detail == null)
                {
                    return RedirectToAction("Error");
                }
                //ارسال
                ViewBag.Detail = detail;
                //آیدی مشتری
                ViewBag.Id = id;
            }
            return View();
        }
        [HttpPost]
        public IActionResult DetailEdit(ViewModelDetail detail)
        {
            var EditDetail = _context.Details.FirstOrDefault(p => p.Detail_Id == detail.Detail_Id);
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

            _context.SaveChanges();

            return RedirectToAction("DetailEdit", new { itemid = detail.Detail_Id, id = detail.Customer_ID });
        }

        #endregion
        
        #region نمایش دسته بندی ها

        public IActionResult Categories(int id)
        {
            var VM = new ViewModel_Product
            {
                Categories = _context.Categories.ToList(),
                Customer_ID = id
            };

            return View(VM);
        }
        #endregion
        
        #region ایجاد دسته بندی
        [HttpPost]
        public IActionResult CategoryCreate(string? Category_NewName, int Customer_ID)
        {
            if (Category_NewName != null)
            {
                var NewCategory = new Category
                {
                    Category_Name = Category_NewName
                };
                _context.Categories.Add(NewCategory);
                _context.SaveChanges();
            }
            return RedirectToAction("Categories", new { id = Customer_ID });
        }
        #endregion
        
        #region  بروزرسانی دسته بندی محصولات
        [HttpPost]
        //تغییر دسته بندی از بین گزینه های موجود
        public IActionResult CategoryUpdate(ProductListUpdateModel model)
        {
            if (model != null)
            {
                foreach (var item in model.Products)
                {
                    var Temp = _context.Products.FirstOrDefault(p => p.Product_Id == item.Product_Id);
                    if (Temp != null)
                    {
                        Temp.Category_ID = item.Category_Id;
                    }
                }
                _context.SaveChanges();
            }

            return RedirectToAction("Products", new { id = model.Customer_ID });
        }
        //تغییرنام دسته بندی های موجود
        public IActionResult CategoryNameUpdate(ListCategories model)
        {
            if (model != null)
            {
                var IDs = model.Categories.Select(p => p.Category_Id).ToList();

                var DB_Categories = _context.Categories.Where(p => IDs.Contains(p.Category_Id)).ToList();

                foreach (var item in model.Categories)
                {
                    var Temp = DB_Categories.FirstOrDefault(p => p.Category_Id == item.Category_Id);
                    if (Temp != null)
                    {
                        Temp.Category_Name = item.Category_NewName;
                    }
                }
                _context.SaveChanges();
            }
            return RedirectToAction("Categories", new { id = model.Customer_ID });
        }
        #endregion 
        
        #region حذف دسته بندی
        public IActionResult CategoryDelete(int CategoryID, int Customer_ID)
        {
            #region تغییر دسته بندی محصولاتی که قرار است دسته بندی آنان حذف شود
            var ProductsCategory_ID = _context.Products.Where(p => p.Category_ID == CategoryID).ToList();
            if (ProductsCategory_ID != null)
            {
                foreach (var items in ProductsCategory_ID)
                {
                    items.Category_ID = 1;
                }
                _context.SaveChanges();
            }
            #endregion

            var item = _context.Categories.Find(CategoryID);
            if (item != null)
            {
                _context.Categories.Remove(item);
            }
            _context.SaveChanges();
            return RedirectToAction("Categories", new { id = Customer_ID });
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
