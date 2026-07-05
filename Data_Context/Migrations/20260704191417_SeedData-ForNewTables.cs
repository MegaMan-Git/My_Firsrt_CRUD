using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Context.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataForNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MyUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), 0, "ONMLKJIHGFEDCBA", "rafieyg2708@gmail.com", true, false, null, "RAFIEYG2708@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEFwOASqH0/Baj2Ys7rs9/R0c1K4eZNTjTLSmoaiNEVVYuyPhkWahpdgJBjJDXlSxGw==", null, false, "ABCDEFGHIJKLMNO", false, "Admin" });

            migrationBuilder.InsertData(
                table: "ApplicationUser_Products",
                columns: new[] { "productId", "userId" },
                values: new object[] { 1, new Guid("11111111-1111-1111-1111-111111111111") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationUser_Products",
                keyColumns: new[] { "productId", "userId" },
                keyValues: new object[] { 1, new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.DeleteData(
                table: "MyUsers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }
    }
}
