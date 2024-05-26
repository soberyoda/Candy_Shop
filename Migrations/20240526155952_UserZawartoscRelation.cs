using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candy_Shop.Migrations
{
    /// <inheritdoc />
    public partial class UserZawartoscRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "Zawartosc",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Zawartosc_username",
                table: "Zawartosc",
                column: "username");

            migrationBuilder.AddForeignKey(
                name: "FK_Zawartosc_Users_username",
                table: "Zawartosc",
                column: "username",
                principalTable: "Users",
                principalColumn: "username",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zawartosc_Users_username",
                table: "Zawartosc");

            migrationBuilder.DropIndex(
                name: "IX_Zawartosc_username",
                table: "Zawartosc");

            migrationBuilder.DropColumn(
                name: "username",
                table: "Zawartosc");
        }
    }
}
