using Microsoft.EntityFrameworkCore.Migrations;

namespace StackOverflow.Migrations
{
    public partial class migration10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Id",
                table: "User",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreatorId",
                table: "Questions",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_CreatorId",
                table: "Answers",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_User_CreatorId",
                table: "Answers",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_User_CreatorId",
                table: "Questions",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_User_CreatorId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_User_CreatorId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CreatorId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_CreatorId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Answers");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserName",
                table: "Questions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserName",
                table: "Answers",
                type: "nvarchar(450)",
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
    }
}
