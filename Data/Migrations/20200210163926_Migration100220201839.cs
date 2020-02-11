using Microsoft.EntityFrameworkCore.Migrations;

namespace SportBox7.Data.Migrations
{
    public partial class Migration100220201839 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeoInfoId",
                table: "Articles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ArticlesSeoData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(nullable: false),
                    MetaTitle = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(nullable: true),
                    MetaKeyword = table.Column<string>(nullable: true),
                    SeoUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlesSeoData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticlesSeoData_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticlesSeoData_ArticleId",
                table: "ArticlesSeoData",
                column: "ArticleId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticlesSeoData");

            migrationBuilder.DropColumn(
                name: "SeoInfoId",
                table: "Articles");
        }
    }
}
