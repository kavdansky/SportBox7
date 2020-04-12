using Microsoft.EntityFrameworkCore.Migrations;

namespace SportBox7.Migrations
{
    public partial class Migration110420201742 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RawArticles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RawArticles");
        }
    }
}
