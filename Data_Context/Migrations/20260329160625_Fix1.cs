using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Context.Migrations
{
    /// <inheritdoc />
    public partial class Fix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Customers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Detail_Id",
                keyValue: 1,
                column: "RegistrationDate",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Customers",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Detail_Id",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2026, 3, 29, 19, 20, 52, 736, DateTimeKind.Local).AddTicks(8342));
        }
    }
}
