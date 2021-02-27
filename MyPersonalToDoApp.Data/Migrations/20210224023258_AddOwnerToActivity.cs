using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPersonalToDoApp.Data.Migrations
{
    public partial class AddOwnerToActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Activities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Activities");
        }
    }
}
