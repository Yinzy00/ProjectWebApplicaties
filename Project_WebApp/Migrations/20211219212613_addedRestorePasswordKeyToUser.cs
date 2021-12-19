using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_WebApp.Migrations
{
    public partial class addedRestorePasswordKeyToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RestorePasswordKey",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestorePasswordKey",
                table: "User");
        }
    }
}
