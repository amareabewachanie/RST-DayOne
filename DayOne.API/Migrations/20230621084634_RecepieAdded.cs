using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DayOne.API.Migrations
{
    public partial class RecepieAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recepie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recepie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientRecepie",
                columns: table => new
                {
                    IngredientsId = table.Column<int>(type: "int", nullable: false),
                    RecepieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientRecepie", x => new { x.IngredientsId, x.RecepieId });
                    table.ForeignKey(
                        name: "FK_IngredientRecepie_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientRecepie_Recepie_RecepieId",
                        column: x => x.RecepieId,
                        principalTable: "Recepie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientRecepie_RecepieId",
                table: "IngredientRecepie",
                column: "RecepieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientRecepie");

            migrationBuilder.DropTable(
                name: "Recepie");
        }
    }
}
