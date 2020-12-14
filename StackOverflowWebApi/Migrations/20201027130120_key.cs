using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StackOverflowWebApi.Migrations
{
    public partial class key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerLiker_Answers_AnswerId",
                table: "AnswerLiker");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerLiker_Users_UserId",
                table: "AnswerLiker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerLiker",
                table: "AnswerLiker");

            migrationBuilder.DropIndex(
                name: "IX_AnswerLiker_UserId",
                table: "AnswerLiker");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "AnswerLiker",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AnswerId",
                table: "AnswerLiker",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerLiker",
                table: "AnswerLiker",
                columns: new[] { "UserId", "AnswerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerLiker_Answers_AnswerId",
                table: "AnswerLiker",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerLiker_Users_UserId",
                table: "AnswerLiker",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerLiker_Answers_AnswerId",
                table: "AnswerLiker");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerLiker_Users_UserId",
                table: "AnswerLiker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerLiker",
                table: "AnswerLiker");

            migrationBuilder.AlterColumn<Guid>(
                name: "AnswerId",
                table: "AnswerLiker",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "AnswerLiker",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerLiker",
                table: "AnswerLiker",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerLiker_UserId",
                table: "AnswerLiker",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerLiker_Answers_AnswerId",
                table: "AnswerLiker",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerLiker_Users_UserId",
                table: "AnswerLiker",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
