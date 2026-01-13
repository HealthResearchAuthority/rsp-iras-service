using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUpdatedProjectModificationAnswersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectModificationAnswers",
                columns: table => new
                {
                    ProjectModificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_ProjectModificationAnswers", x => new { x.ProjectModificationId, x.QuestionId, x.ProjectPersonnelId });
                    table.ForeignKey(
                        name: "FK_ProjectModificationAnswers_ProjectModifications_ProjectModificationId",
                        column: x => x.ProjectModificationId,
                        principalTable: "ProjectModifications",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectModificationAnswers");
        }
    }
}
