using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_WebApp.Migrations
{
    public partial class FixMessageUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_UserId1",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_UserId1",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Message");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Message",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserId",
                table: "Message",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_UserId",
                table: "Message",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_UserId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_UserId",
                table: "Message");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Message",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Message",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserId1",
                table: "Message",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_UserId1",
                table: "Message",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
