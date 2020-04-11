using Microsoft.EntityFrameworkCore.Migrations;

namespace MyAspNetProject.Migrations
{
    public partial class ThingsName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ThingsNedded",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ThingsNedded");
        }
    }
}
