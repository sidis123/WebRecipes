using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRecipesBE.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id_Kategorija = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pavadinimas = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id_Kategorija);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id_Vartotojas = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vardas = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    pavarde = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    telefonas = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id_Vartotojas);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    id_Receptas = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pavadinimas = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    tekstas = table.Column<string>(type: "nvarchar(254)", nullable: false),
                    instrukcija = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Userid_Vartotojas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.id_Receptas);
                    table.ForeignKey(
                        name: "FK_Recipes_Users_Userid_Vartotojas",
                        column: x => x.Userid_Vartotojas,
                        principalTable: "Users",
                        principalColumn: "id_Vartotojas",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    id_Komentaras = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patiktukai = table.Column<int>(type: "int", nullable: false),
                    sukurimo_data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tekstas = table.Column<string>(type: "nvarchar(254)", nullable: false),
                    Userid_Vartotojas = table.Column<int>(type: "int", nullable: false),
                    Recipeid_Receptas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.id_Komentaras);
                    table.ForeignKey(
                        name: "FK_Comments_Recipes_Recipeid_Receptas",
                        column: x => x.Recipeid_Receptas,
                        principalTable: "Recipes",
                        principalColumn: "id_Receptas",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_Userid_Vartotojas",
                        column: x => x.Userid_Vartotojas,
                        principalTable: "Users",
                        principalColumn: "id_Vartotojas",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeCategories",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeCategories", x => new { x.RecipeId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_RecipeCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "id_Kategorija",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeCategories_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "id_Receptas",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Recipeid_Receptas",
                table: "Comments",
                column: "Recipeid_Receptas");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Userid_Vartotojas",
                table: "Comments",
                column: "Userid_Vartotojas");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeCategories_CategoryId",
                table: "RecipeCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_Userid_Vartotojas",
                table: "Recipes",
                column: "Userid_Vartotojas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "RecipeCategories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
