using Microsoft.EntityFrameworkCore.Migrations;

namespace SportBox7.Migrations
{
    public partial class Migration100420201428 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "RawArticles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "RawArticles");
        }
    }
}
