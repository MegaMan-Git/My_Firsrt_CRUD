using Data_Context.Data;
using ModelAss.ColorOptions_ForDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelAss.ViewList
{
    public class ViewModel_Product
    {
        public int Customer_ID {  get; set; }

        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Detail> Details { get; set; }
        public List<Customer> Customers { get; set; }
    }

    #region ساخت محصول
    public class ViewModel_CreateProduct
    {
        public int Customer_ID { get; set; }

        public string Product_Name {  get; set; }
        public int Product_Price {  get; set; }
        public int Category_ID {  get; set; }
        public int In_Stock {  get; set; }
        public string Detail_Description { get; set; }
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

    // مدلی که لیست محصولات به ‌روزرسانی شده را در فرم دریافت می‌کند
    public class ProductListUpdateModel
    {
        public int Customer_ID { get; set; }

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
        public int Customer_ID { get; set; }

        public List<Categories> Categories { get; set; }
    }
    #endregion

    #region تغییر جزئیات یک محصول
    public class ViewModelDetail
    {
        public int Customer_ID { get; set; }

        public int Detail_Id {  get; set; }
        public int In_Stock { get; set; }
        public string Detail_Description {  get; set; }
        public ColorOptions Detail_Color {  get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
    #endregion

}
