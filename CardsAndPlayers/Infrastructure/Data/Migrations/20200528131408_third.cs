using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Players");

            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Players",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NickName",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Players");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
