using Microsoft.EntityFrameworkCore.Migrations;
using ModelAss.ColorOptions_ForDetail;

#nullable disable

namespace Data_Context.Migrations
{
    public partial class SeedData2 : Migration
    {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
            // اضافه کردن داده به جدول Details
            migrationBuilder.InsertData(
                table: "Details",
                columns: new[] { "Detail_Id", "Detail_Color", "In_Stock", "Detail_Description", "RegistrationDate" },
                values: new object[] {
                         1, // فرض می‌کنیم Detail_Id برابر 1 است
                         ((int)ColorOptions.Red).ToString(), // مقدار عددی ColorOptions.Red را اینجا قرار دهید
                         10, // مقدار In_Stock
                         "Made From Mazandaran", // Detail_Description
                         new DateTime(2026, 3, 29, 12, 0, 0, DateTimeKind.Utc) // مقدار ثابت DateTime
                });

        // اضافه کردن داده به جدول Customers
        migrationBuilder.InsertData(
            table: "Customers",
            columns: new[] { "Customer_Id", "FirstName", "LastName", "Password", "Address", "NumberPhone" },
            values: new object[] {
                         1, // فرض می‌کنیم Customer_Id برابر 1 است
                         "AmirMohammad", // FirstName
                         "Rafiey", // LastName
                         "1234", // Password
                         "Tehran", // Address
                         "09123456789" // NumberPhone
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // حذف داده از جدول Customers
        migrationBuilder.DeleteData(
            table: "Customers",
            keyColumn: "Customer_Id",
            keyValues: new object[] { 1 });

        // حذف داده از جدول Details
        migrationBuilder.DeleteData(
            table: "Details",
            keyColumn: "Detail_Id",
            keyValues: new object[] { 1 });
    }
}

     }
