using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPersonalToDoApp.Data.Migrations
{
    public partial class EnableActivityCustomerLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Activities");

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "Activities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CustomerId",
                table: "Activities",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Customers_CustomerId",
                table: "Activities",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Customers_CustomerId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_CustomerId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Activities");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Activities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");
        }
    }
}
