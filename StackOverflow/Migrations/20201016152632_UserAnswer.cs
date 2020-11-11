using Microsoft.EntityFrameworkCore.Migrations;

namespace StackOverflow.Migrations
{
    public partial class UserAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "User",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                table: "User",
                type: "real",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
