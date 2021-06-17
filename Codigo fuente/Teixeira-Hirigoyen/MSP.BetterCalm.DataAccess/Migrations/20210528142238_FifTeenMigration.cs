using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace MSP.BetterCalm.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class FifTeenMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    MinSeconds = table.Column<double>(type: "float", nullable: false),
                    LinkVideo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryVideo",
                columns: table => new
                {
                    IdCategory = table.Column<int>(type: "int", nullable: false),
                    IdVideo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryVideo", x => new { x.IdCategory, x.IdVideo });
                    table.ForeignKey(
                        name: "FK_CategoryVideo_Category_IdCategory",
                        column: x => x.IdCategory,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryVideo_Video_IdVideo",
                        column: x => x.IdVideo,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistVideo",
                columns: table => new
                {
                    IdPlaylist = table.Column<int>(type: "int", nullable: false),
                    IdVideo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistVideo", x => new { x.IdPlaylist, x.IdVideo });
                    table.ForeignKey(
                        name: "FK_PlaylistVideo_Playlist_IdPlaylist",
                        column: x => x.IdPlaylist,
                        principalTable: "Playlist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistVideo_Video_IdVideo",
                        column: x => x.IdVideo,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryVideo_IdVideo",
                table: "CategoryVideo",
                column: "IdVideo");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistVideo_IdVideo",
                table: "PlaylistVideo",
                column: "IdVideo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryVideo");

            migrationBuilder.DropTable(
                name: "PlaylistVideo");

            migrationBuilder.DropTable(
                name: "Video");
        }
    }
}
