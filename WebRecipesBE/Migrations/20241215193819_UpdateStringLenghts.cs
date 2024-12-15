using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRecipesBE.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStringLenghts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "vardas",
                table: "Users",
                type: "nvarchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "telefonas",
                table: "Users",
                type: "nvarchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "pavarde",
                table: "Users",
                type: "nvarchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "Users",
                type: "nvarchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "Users",
                type: "nvarchar(900)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "tekstas",
                table: "Recipes",
                type: "nvarchar(3999)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(254)");

            migrationBuilder.AlterColumn<string>(
                name: "pavadinimas",
                table: "Recipes",
                type: "nvarchar(3999)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "instrukcija",
                table: "Recipes",
                type: "nvarchar(3999)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "tekstas",
                table: "Comments",
                type: "nvarchar(3999)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(254)");

            migrationBuilder.AlterColumn<string>(
                name: "pavadinimas",
                table: "Categories",
                type: "nvarchar(3999)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Recipes");

            migrationBuilder.AlterColumn<string>(
                name: "vardas",
                table: "Users",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "telefonas",
                table: "Users",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "pavarde",
                table: "Users",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "Users",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "Users",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(900)");

            migrationBuilder.AlterColumn<string>(
                name: "tekstas",
                table: "Recipes",
                type: "nvarchar(254)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5000)");

            migrationBuilder.AlterColumn<string>(
                name: "pavadinimas",
                table: "Recipes",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5000)");

            migrationBuilder.AlterColumn<string>(
                name: "instrukcija",
                table: "Recipes",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5000)");

            migrationBuilder.AlterColumn<string>(
                name: "tekstas",
                table: "Comments",
                type: "nvarchar(254)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5000)");

            migrationBuilder.AlterColumn<string>(
                name: "pavadinimas",
                table: "Categories",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5000)");
        }
    }
}
