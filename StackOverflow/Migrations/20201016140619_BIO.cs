using Microsoft.EntityFrameworkCore.Migrations;

namespace StackOverflow.Migrations
{
    public partial class BIO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "User");
        }
    }
}
