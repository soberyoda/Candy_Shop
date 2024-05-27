using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candy_Shop.Migrations
{
    /// <inheritdoc />
    public partial class order2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_id_zawartosci",
                table: "Orders",
                column: "id_zawartosci");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_username",
                table: "Orders",
                column: "username");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_username",
                table: "Orders",
                column: "username",
                principalTable: "Users",
                principalColumn: "username",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Zawartosc_id_zawartosci",
                table: "Orders",
                column: "id_zawartosci",
                principalTable: "Zawartosc",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_username",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Zawartosc_id_zawartosci",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_id_zawartosci",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_username",
                table: "Orders");
        }
    }
}
