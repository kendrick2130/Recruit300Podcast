using Microsoft.EntityFrameworkCore.Migrations;

namespace Recruit300Podcast.Migrations
{
    public partial class Initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Podcasts",
                columns: table => new
                {
                    PodcastId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    Copyright = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    OwnerName = table.Column<string>(nullable: true),
                    OwnerEmail = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    ExplicitBool = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Podcasts", x => x.PodcastId);
                });

            migrationBuilder.CreateTable(
                name: "PodcastEpisodes",
                columns: table => new
                {
                    Guide = table.Column<string>(nullable: false),
                    EpisodeType = table.Column<string>(nullable: true),
                    PodcastId = table.Column<int>(nullable: false),
                    Season = table.Column<int>(nullable: false),
                    EpisodeNumber = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Length = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Publicationdate = table.Column<string>(nullable: true),
                    Duration = table.Column<string>(nullable: true),
                    ExplicitBool = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PodcastEpisodes", x => x.Guide);
                    table.ForeignKey(
                        name: "FK_PodcastEpisodes_Podcasts_PodcastId",
                        column: x => x.PodcastId,
                        principalTable: "Podcasts",
                        principalColumn: "PodcastId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PodcastEpisodes_PodcastId",
                table: "PodcastEpisodes",
                column: "PodcastId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PodcastEpisodes");

            migrationBuilder.DropTable(
                name: "Podcasts");
        }
    }
}
