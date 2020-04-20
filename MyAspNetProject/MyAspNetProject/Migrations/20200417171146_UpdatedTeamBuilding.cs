using Microsoft.EntityFrameworkCore.Migrations;

namespace MyAspNetProject.Migrations
{
    public partial class UpdatedTeamBuilding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "TeamBuilding",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "TeamBuilding");
        }
    }
}
