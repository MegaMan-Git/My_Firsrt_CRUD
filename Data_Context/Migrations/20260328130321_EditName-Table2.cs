using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Context.Migrations
{
    /// <inheritdoc />
    public partial class EditNameTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Product_customers_Customer_id",
                table: "Customer_Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Product_products_Product_id",
                table: "Customer_Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Details_products_Detail_Id",
                table: "Details");

            migrationBuilder.DropForeignKey(
                name: "FK_products_Categories_Category_ID",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customers",
                table: "customers");

            migrationBuilder.RenameTable(
                name: "products",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "customers",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_products_Category_ID",
                table: "Products",
                newName: "IX_Products_Category_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Product_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Customer_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Product_Customers_Customer_id",
                table: "Customer_Product",
                column: "Customer_id",
                principalTable: "Customers",
                principalColumn: "Customer_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Product_Products_Product_id",
                table: "Customer_Product",
                column: "Product_id",
                principalTable: "Products",
                principalColumn: "Product_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Details_Products_Detail_Id",
                table: "Details",
                column: "Detail_Id",
                principalTable: "Products",
                principalColumn: "Product_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_Category_ID",
                table: "Products",
                column: "Category_ID",
                principalTable: "Categories",
                principalColumn: "Category_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Product_Customers_Customer_id",
                table: "Customer_Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Product_Products_Product_id",
                table: "Customer_Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Details_Products_Detail_Id",
                table: "Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_Category_ID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "products");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "customers");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Category_ID",
                table: "products",
                newName: "IX_products_Category_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "Product_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customers",
                table: "customers",
                column: "Customer_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Product_customers_Customer_id",
                table: "Customer_Product",
                column: "Customer_id",
                principalTable: "customers",
                principalColumn: "Customer_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Product_products_Product_id",
                table: "Customer_Product",
                column: "Product_id",
                principalTable: "products",
                principalColumn: "Product_Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
