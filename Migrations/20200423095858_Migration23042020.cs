using Microsoft.EntityFrameworkCore.Migrations;

namespace SportBox7.Migrations
{
    public partial class Migration23042020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.CreateTable(
                name: "SocialSignals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(nullable: false),
                    IsLiked = table.Column<bool>(nullable: false),
                    VisitorIp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialSignals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialSignals_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocialSignals_ArticleId",
                table: "SocialSignals",
                column: "ArticleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialSignals");

            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeagueName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeagueNameAlternate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeagueNameInBulgarian = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SportName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SportNameInBulgarian = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.Id);
                });
        }
    }
}
