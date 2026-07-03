using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Context.Migrations
{
    /// <inheritdoc />
    public partial class EditNameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_Details_products_Detail_Id",
                table: "product_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_products_product_Categories_Category_ID",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_Details",
                table: "product_Details");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_Categories",
                table: "product_Categories");

            migrationBuilder.RenameTable(
                name: "product_Details",
                newName: "Details");

            migrationBuilder.RenameTable(
                name: "product_Categories",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Details",
                table: "Details",
                column: "Detail_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Category_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Details_products_Detail_Id",
                table: "Details",
                column: "Detail_Id",
                principalTable: "products",
                principalColumn: "Product_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_Categories_Category_ID",
                table: "products",
                column: "Category_ID",
                principalTable: "Categories",
                principalColumn: "Category_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Details_products_Detail_Id",
                table: "Details");

            migrationBuilder.DropForeignKey(
                name: "FK_products_Categories_Category_ID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Details",
                table: "Details");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Details",
                newName: "product_Details");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "product_Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_Details",
                table: "product_Details",
                column: "Detail_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_Categories",
                table: "product_Categories",
                column: "Category_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_Details_products_Detail_Id",
                table: "product_Details",
                column: "Detail_Id",
                principalTable: "products",
                principalColumn: "Product_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_product_Categories_Category_ID",
                table: "products",
                column: "Category_ID",
                principalTable: "product_Categories",
                principalColumn: "Category_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
