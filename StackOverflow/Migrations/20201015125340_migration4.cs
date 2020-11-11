using Microsoft.EntityFrameworkCore.Migrations;

namespace StackOverflow.Migrations
{
    public partial class migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_User_CreatorLogin",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_User_CreatorLogin",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CreatorLogin",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_CreatorLogin",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "CreatorLogin",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CreatorLogin",
                table: "Answers");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "User",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserName",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserName",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreatorUserName",
                table: "Questions",
                column: "CreatorUserName");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_CreatorUserName",
                table: "Answers",
                column: "CreatorUserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_User_CreatorUserName",
                table: "Answers",
                column: "CreatorUserName",
                principalTable: "User",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_User_CreatorUserName",
                table: "Questions",
                column: "CreatorUserName",
                principalTable: "User",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_User_CreatorUserName",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_User_CreatorUserName",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CreatorUserName",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_CreatorUserName",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "CreatorUserName",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CreatorUserName",
                table: "Answers");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "User",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "CreatorLogin",
                table: "Questions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorLogin",
                table: "Answers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Login");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreatorLogin",
                table: "Questions",
                column: "CreatorLogin");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_CreatorLogin",
                table: "Answers",
                column: "CreatorLogin");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_User_CreatorLogin",
                table: "Answers",
                column: "CreatorLogin",
                principalTable: "User",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_User_CreatorLogin",
                table: "Questions",
                column: "CreatorLogin",
                principalTable: "User",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
