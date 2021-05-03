using Microsoft.EntityFrameworkCore.Migrations;

namespace MSP.BetterCalm.DataAccess.Migrations
{
    public partial class ThirdTeenMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
