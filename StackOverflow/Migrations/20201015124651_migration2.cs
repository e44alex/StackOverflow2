using Microsoft.EntityFrameworkCore.Migrations;

namespace StackOverflow.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                table: "User",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<float>(
                name: "Exerience",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exerience",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "User");

            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                table: "User",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);
        }
    }
}
