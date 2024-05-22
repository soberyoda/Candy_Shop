using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Candy_Shop.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Czekoladki",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nazwa = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    czekolada = table.Column<int>(type: "INTEGER", nullable: false),
                    orzechy = table.Column<int>(type: "INTEGER", nullable: true),
                    opis = table.Column<string>(type: "TEXT", nullable: false),
                    cena = table.Column<decimal>(type: "TEXT", nullable: false),
                    masa = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Czekoladki", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Zawartosc",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    sztuk = table.Column<int>(type: "INTEGER", nullable: false),
                    id_czekoladki = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zawartosc", x => x.id);
                    table.ForeignKey(
                        name: "FK_Zawartosc_Czekoladki_id_czekoladki",
                        column: x => x.id_czekoladki,
                        principalTable: "Czekoladki",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Czekoladki",
                columns: new[] { "id", "cena", "czekolada", "masa", "nazwa", "opis", "orzechy" },
                values: new object[,]
                {
                    { 1, 15.32m, 1, 0.53m, "Testowa", "Testowa czekoladka", 1 },
                    { 2, 15.32m, 2, 0.53m, "Testowa 2", "Kolejna czekoladka testowa", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zawartosc_id_czekoladki",
                table: "Zawartosc",
                column: "id_czekoladki");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zawartosc");

            migrationBuilder.DropTable(
                name: "Czekoladki");
        }
    }
}
