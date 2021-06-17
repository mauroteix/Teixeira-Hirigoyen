using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace MSP.BetterCalm.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class FourTeenMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MeetingDuration",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MeetingPrice",
                table: "Psychologist",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MeetingDuration",
                table: "Meeting",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalPrice",
                table: "Meeting",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeetingDuration",
                table: "User");

            migrationBuilder.DropColumn(
                name: "MeetingPrice",
                table: "Psychologist");

            migrationBuilder.DropColumn(
                name: "MeetingDuration",
                table: "Meeting");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Meeting");
        }
    }
}
