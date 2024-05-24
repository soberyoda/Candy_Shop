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
            migrationBuilder.CreateTable(
              name: "Users",
              columns: table => new
              {
                username = table.Column<string>(type: "TEXT", nullable: false),
                password = table.Column<string>(type: "TEXT", nullable: false),
                apiToken = table.Column<string>(type: "TEXT", nullable: false),
                type = table.Column<int>(type: "INTEGER", nullable: false)
              },
              constraints: table =>
              {
                table.PrimaryKey("PK_Users", x => x.username);
              });
            migrationBuilder.InsertData(
              table: "Users",
              columns: new[] { "username", "apiToken", "password", "type" },
              values: new object[] { "admin", "aae1e103-bca5-9fa1-ba8c-42058b4abf28", "21232F297A57A5A743894A0E4A801FC3", 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          migrationBuilder.DropTable(
            name: "Users");
        }
    }
}
