using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StackOverflow.Migrations
{
    public partial class LikersList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikesCount",
                table: "Answers");

            migrationBuilder.AlterColumn<string>(
                name: "Topic",
                table: "Questions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "Questions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "Answers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AnswerLiker",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    AnswerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerLiker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerLiker_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswerLiker_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerLiker_AnswerId",
                table: "AnswerLiker",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerLiker_UserId",
                table: "AnswerLiker",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerLiker");

            migrationBuilder.AlterColumn<string>(
                name: "Topic",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "LikesCount",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
