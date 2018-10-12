using Microsoft.EntityFrameworkCore.Migrations;

namespace petgoods4all.Migrations
{
    public partial class AdminColumnAddedToAccountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "Account",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Admin",
                table: "Account");
        }
    }
}
