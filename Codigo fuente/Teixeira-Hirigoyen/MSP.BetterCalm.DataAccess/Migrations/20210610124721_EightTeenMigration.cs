﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace MSP.BetterCalm.DataAccess.Migrations
{
    public partial class EightTeenMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "MedicalCondition",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "MedicalCondition");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Category");
        }
    }
}
