using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelAss.ViewList
{
    #region  ثبت نام
    public class ViewModel_SignUpUser
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
    #endregion

    #region ورود
    public class ViewModel_LoginUser
    {
        [Required(ErrorMessage = "نام کاربری الزامی هست"), MaxLength(128)]
        public string UserName { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }   
    }
    #endregion

    #region تعویض رمز عبور

    public class ForgotPassword_VM
    {
        [Required(ErrorMessage = "ایمیل الزامی است")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نیست")]
        public string Email { get; set; }
    }
    public class ResetPassword_VM
    {
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword {  get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword),ErrorMessage ="رمز عبور تطابق ندارد")]
        public string ConfirmNewPassword {  get; set; }
        
        [Required]
        public string Email {get; set; }
        [Required]
        public string Token { get; set; }
    }
    #endregion
}
