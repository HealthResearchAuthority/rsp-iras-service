using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameProjectModificationAnswersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectModificationAnswers");

            migrationBuilder.CreateTable(
                name: "ProjectModificationChangeAnswers",
                columns: table => new
                {
                    ProjectModificationChangeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectPersonnelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VersionId = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "published version"),
                    ProjectRecordId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectedOptions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectModificationChangeAnswers", x => new { x.ProjectModificationChangeId, x.QuestionId, x.ProjectPersonnelId });
                    table.ForeignKey(
                        name: "FK_ProjectModificationChangeAnswers_ProjectModificationChanges_ProjectModificationChangeId",
                        column: x => x.ProjectModificationChangeId,
                        principalTable: "ProjectModificationChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectModificationChangeAnswers_ProjectPersonnels_ProjectPersonnelId",
                        column: x => x.ProjectPersonnelId,
                        principalTable: "ProjectPersonnels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectModificationChangeAnswers_ProjectRecords_ProjectRecordId",
                        column: x => x.ProjectRecordId,
                        principalTable: "ProjectRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationChangeAnswers_ProjectPersonnelId",
                table: "ProjectModificationChangeAnswers",
                column: "ProjectPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationChangeAnswers_ProjectRecordId",
                table: "ProjectModificationChangeAnswers",
                column: "ProjectRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectModificationChangeAnswers");

            migrationBuilder.CreateTable(
                name: "ProjectModificationAnswers",
                columns: table => new
                {
                    ProjectModificationChangeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectPersonnelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectRecordId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelectedOptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersionId = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "published version")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectModificationAnswers", x => new { x.ProjectModificationChangeId, x.QuestionId, x.ProjectPersonnelId });
                    table.ForeignKey(
                        name: "FK_ProjectModificationAnswers_ProjectModificationChanges_ProjectModificationChangeId",
                        column: x => x.ProjectModificationChangeId,
                        principalTable: "ProjectModificationChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectModificationAnswers_ProjectPersonnels_ProjectPersonnelId",
                        column: x => x.ProjectPersonnelId,
                        principalTable: "ProjectPersonnels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectModificationAnswers_ProjectRecords_ProjectRecordId",
                        column: x => x.ProjectRecordId,
                        principalTable: "ProjectRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationAnswers_ProjectPersonnelId",
                table: "ProjectModificationAnswers",
                column: "ProjectPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationAnswers_ProjectRecordId",
                table: "ProjectModificationAnswers",
                column: "ProjectRecordId");
        }
    }
}
