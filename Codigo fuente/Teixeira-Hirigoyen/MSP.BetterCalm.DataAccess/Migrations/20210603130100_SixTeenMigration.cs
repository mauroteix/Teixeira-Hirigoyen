using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace MSP.BetterCalm.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class SixTeenMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MeetingCount",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MeetingDiscount",
                table: "Meeting",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "User");

            migrationBuilder.DropColumn(
                name: "MeetingCount",
                table: "User");

            migrationBuilder.DropColumn(
                name: "MeetingDiscount",
                table: "Meeting");
        }
    }
}
