using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace MSP.BetterCalm.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class EightMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicalConditionId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_MedicalConditionId",
                table: "User",
                column: "MedicalConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_MedicalCondition_MedicalConditionId",
                table: "User",
                column: "MedicalConditionId",
                principalTable: "MedicalCondition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_MedicalCondition_MedicalConditionId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_MedicalConditionId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "MedicalConditionId",
                table: "User");
        }
    }
}
