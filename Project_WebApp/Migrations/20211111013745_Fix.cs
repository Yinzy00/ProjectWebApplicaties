using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_WebApp.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Subject_SubjectId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_SubjectId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Message");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Message",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_SubjectId",
                table: "Message",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Subject_SubjectId",
                table: "Message",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
