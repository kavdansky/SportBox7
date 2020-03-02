using Microsoft.EntityFrameworkCore.Migrations;

namespace SportBox7.Migrations
{
    public partial class Migration020320201704 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "H1Tag",
                table: "Articles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TempArticleId",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "H1Tag",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "TempArticleId",
                table: "Articles");
        }
    }
}
