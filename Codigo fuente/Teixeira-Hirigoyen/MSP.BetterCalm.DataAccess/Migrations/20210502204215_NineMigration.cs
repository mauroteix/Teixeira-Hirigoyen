using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace MSP.BetterCalm.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class NineMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdressMeeting",
                table: "Psychologist");

            migrationBuilder.AddColumn<string>(
                name: "AdressMeeting",
                table: "Meeting",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdressMeeting",
                table: "Meeting");

            migrationBuilder.AddColumn<string>(
                name: "AdressMeeting",
                table: "Psychologist",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
