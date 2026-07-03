using Microsoft.EntityFrameworkCore.Migrations;
using ModelAss.ColorOptions_ForDetail;

#nullable disable

namespace Data_Context.Migrations
{
    /// <inheritdoc />
    public partial class SeedData3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
    table: "Products",
    columns: new[] { "Product_Id", "Product_Name", "Product_Price", "Category_ID" },
    values: new object[] { 1, "Apple", 10000, 1 });

            migrationBuilder.InsertData(
                table: "Details",
                columns: new[] { "Detail_Id", "Detail_Color", "In_Stock", "Detail_Description", "RegistrationDate" },
                values: new object[] { 
                    1,
                    ((int)ColorOptions.Red).ToString(),
                    10,
                    "Made From Mazandaran",
                    new DateTime(2026, 3, 29, 12, 0, 0, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
