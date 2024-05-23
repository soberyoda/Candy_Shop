using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candy_Shop.Migrations
{
    /// <inheritdoc />
    public partial class CreateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "username",
                keyValue: "admin",
                column: "type",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "username",
                keyValue: "admin",
                column: "type",
                value: 1);
        }
    }
}
