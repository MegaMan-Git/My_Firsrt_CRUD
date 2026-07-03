using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Context.Migrations
{
    /// <inheritdoc />
    public partial class DbSetFixName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_Products_Customers_Customer_id",
                table: "customer_Products");

            migrationBuilder.DropForeignKey(
                name: "FK_customer_Products_Products_Product_id",
                table: "customer_Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customer_Products",
                table: "customer_Products");

            migrationBuilder.RenameTable(
                name: "customer_Products",
                newName: "Customer_Products");

            migrationBuilder.RenameIndex(
                name: "IX_customer_Products_Customer_id",
                table: "Customer_Products",
                newName: "IX_Customer_Products_Customer_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer_Products",
                table: "Customer_Products",
                columns: new[] { "Product_id", "Customer_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Products_Customers_Customer_id",
                table: "Customer_Products",
                column: "Customer_id",
                principalTable: "Customers",
                principalColumn: "Customer_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Products_Products_Product_id",
                table: "Customer_Products",
                column: "Product_id",
                principalTable: "Products",
                principalColumn: "Product_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Products_Customers_Customer_id",
                table: "Customer_Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Products_Products_Product_id",
                table: "Customer_Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer_Products",
                table: "Customer_Products");

            migrationBuilder.RenameTable(
                name: "Customer_Products",
                newName: "customer_Products");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_Products_Customer_id",
                table: "customer_Products",
                newName: "IX_customer_Products_Customer_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customer_Products",
                table: "customer_Products",
                columns: new[] { "Product_id", "Customer_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_customer_Products_Customers_Customer_id",
                table: "customer_Products",
                column: "Customer_id",
                principalTable: "Customers",
                principalColumn: "Customer_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_customer_Products_Products_Product_id",
                table: "customer_Products",
                column: "Product_id",
                principalTable: "Products",
                principalColumn: "Product_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
