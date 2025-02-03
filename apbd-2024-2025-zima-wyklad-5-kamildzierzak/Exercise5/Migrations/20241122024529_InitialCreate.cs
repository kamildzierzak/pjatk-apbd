using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Exercise5.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    AnimalId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Category = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Area = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.AnimalId);
                });

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "AnimalId", "Area", "Category", "Description", "Name" },
                values: new object[,]
                {
                    { 1L, "Świat", "Ssak", "Najlepszy pies na świecie!", "Pies" },
                    { 2L, "Antarktyda", "Ptak", "Ptak nielot, ale pływa.", "Pingwin" },
                    { 3L, "Afryka, Azja", "Ssak", "Duży z trąbą.", "Słoń" },
                    { 4L, "Oceany", "Ryba", "Szczęki trzy!", "Rekin" },
                    { 5L, "Świat", "Ptak", "Ptak co fruwa.", "Orzeł" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animal");
        }
    }
}
