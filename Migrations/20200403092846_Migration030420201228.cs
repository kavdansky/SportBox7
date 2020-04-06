using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportBox7.Migrations
{
    public partial class Migration030420201228 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserAdCode",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RawArticles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    H1Tag = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    SourceURL = table.Column<string>(nullable: true),
                    SourceName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawArticles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RawArticles");

            migrationBuilder.DropColumn(
                name: "UserAdCode",
                table: "AspNetUsers");
        }
    }
}
