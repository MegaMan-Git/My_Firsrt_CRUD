using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Context.Migrations
{
    /// <inheritdoc />
    public partial class Fix_CategoryAndProductSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Category_Id", "Category_Name" },
                values: new object[] { 1, "Fruits" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Product_Id", "Category_ID", "Product_Name", "Product_Price" },
                values: new object[] { 1, 1, "Apple", 10000 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
