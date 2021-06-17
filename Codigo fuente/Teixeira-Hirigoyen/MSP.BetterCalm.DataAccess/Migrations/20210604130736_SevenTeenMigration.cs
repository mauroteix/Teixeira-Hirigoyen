using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace MSP.BetterCalm.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class SevenTeenMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Meeting",
                table: "Meeting");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Meeting",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meeting",
                table: "Meeting",
                columns: new[] { "IdPsychologist", "IdUser", "Id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Meeting",
                table: "Meeting");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Meeting");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meeting",
                table: "Meeting",
                columns: new[] { "IdPsychologist", "IdUser" });
        }
    }
}
