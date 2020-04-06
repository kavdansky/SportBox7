using Microsoft.EntityFrameworkCore.Migrations;

namespace SportBox7.Migrations
{
    public partial class Migration040420201305 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDraft",
                table: "Articles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDraft",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
