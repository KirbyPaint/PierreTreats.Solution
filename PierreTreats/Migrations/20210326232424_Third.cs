using Microsoft.EntityFrameworkCore.Migrations;

namespace PierreTreats.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ingredients",
                table: "Treats");

            migrationBuilder.RenameColumn(
                name: "Instructions",
                table: "Treats",
                newName: "Price");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Treats",
                newName: "Instructions");

            migrationBuilder.AddColumn<string>(
                name: "Ingredients",
                table: "Treats",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
