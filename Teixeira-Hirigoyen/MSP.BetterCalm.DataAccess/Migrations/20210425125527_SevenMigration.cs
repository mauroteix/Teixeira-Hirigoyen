using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSP.BetterCalm.DataAccess.Migrations
{
    public partial class SevenMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PsyExpertise");

            migrationBuilder.CreateTable(
                name: "Expertise",
                columns: table => new
                {
                    IdMedicalCondition = table.Column<int>(type: "int", nullable: false),
                    IdPsychologist = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expertise", x => new { x.IdPsychologist, x.IdMedicalCondition });
                    table.ForeignKey(
                        name: "FK_Expertise_MedicalCondition_IdMedicalCondition",
                        column: x => x.IdMedicalCondition,
                        principalTable: "MedicalCondition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expertise_Psychologist_IdPsychologist",
                        column: x => x.IdPsychologist,
                        principalTable: "Psychologist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meeting",
                columns: table => new
                {
                    IdMeeting = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdPsychologist = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meeting", x => new { x.IdPsychologist, x.IdUser, x.IdMeeting });
                    table.ForeignKey(
                        name: "FK_Meeting_Psychologist_IdPsychologist",
                        column: x => x.IdPsychologist,
                        principalTable: "Psychologist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meeting_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expertise_IdMedicalCondition",
                table: "Expertise",
                column: "IdMedicalCondition");

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_IdUser",
                table: "Meeting",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expertise");

            migrationBuilder.DropTable(
                name: "Meeting");

            migrationBuilder.CreateTable(
                name: "PsyExpertise",
                columns: table => new
                {
                    IdPsychologist = table.Column<int>(type: "int", nullable: false),
                    IdMedicalCondition = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsyExpertise", x => new { x.IdPsychologist, x.IdMedicalCondition });
                    table.ForeignKey(
                        name: "FK_PsyExpertise_MedicalCondition_IdMedicalCondition",
                        column: x => x.IdMedicalCondition,
                        principalTable: "MedicalCondition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PsyExpertise_Psychologist_IdPsychologist",
                        column: x => x.IdPsychologist,
                        principalTable: "Psychologist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PsyExpertise_IdMedicalCondition",
                table: "PsyExpertise",
                column: "IdMedicalCondition");
        }
    }
}
