using Data_Context.Data;
using ModelAss.ColorOptions_ForDetail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelAss.ViewList
{
    public class ViewModel_Product
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Detail> Details { get; set; }
        
    }

    #region ساخت محصول
    public class ViewModel_CreateProduct
    {
        [Required]
        public string Product_Name {  get; set; }
        [Required]
        public int Product_Price {  get; set; }
        [Required]
        public int Category_ID {  get; set; }
        [Required]
        public int In_Stock {  get; set; }
        [Required]
        public string Detail_Description { get; set; }
        [Required]
        public ColorOptions Detail_Color {  get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
    #endregion

    #region بروزرسانی دسته بندی هر محصول
    public class ProductUpdateModel
    {
        public int Product_Id { get; set; }
        public int Category_Id { get; set; } 
    }

    // مدلی که لیست محصولات به روزرسانی شده را در فرم دریافت میکند
    public class ProductListUpdateModel
    {
        public List<ProductUpdateModel> Products { get; set; }
    }
    #endregion

    #region تغییر نام دسته بندی ها
    public class Categories
    {
        public int Category_Id { get; set; }
        public string Category_NewName { get; set; }
    }
    public class ListCategories
    {
        public List<Categories> Categories { get; set; }
    }
    #endregion

    #region تغییر جزئیات یک محصول
    public class ViewModelDetail
    {
        public int Detail_Id {  get; set; }
        public int In_Stock { get; set; }
        public string Detail_Description {  get; set; }
        public ColorOptions Detail_Color {  get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
    #endregion

}
