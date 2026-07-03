using System;
using System.Collections.Generic;
using System.Text;
using ModelAss.ColorOptions_ForDetail;

namespace Data_Context.Data
{
    public class Detail
    {
        public int Detail_Id { get; set; }
        public int In_Stock {  get; set; }
        public string Detail_Description { get; set; }
        public ColorOptions Detail_Color {  get; set; }
        // علامت سوال باعث میشود که مقدار پیش فرض آن تهی باشد
        public DateTime? RegistrationDate {  get; set; }

        public Product product { get; set; }
    }
}
