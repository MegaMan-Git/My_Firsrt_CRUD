using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Context.Migrations
{
    /// <inheritdoc />
    public partial class Fix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Detail_Id",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2026, 3, 29, 12, 0, 0, 0, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "Detail_Id",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2026, 3, 29, 19, 43, 57, 460, DateTimeKind.Local).AddTicks(4366));
        }
    }
}
