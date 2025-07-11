using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModificationDataModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VersionId",
                table: "ProjectRecordAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VersionId",
                table: "ProjectModificationAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModificationParticipatingOrganisations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectModificationChangeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganisationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectPersonnelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectRecordId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificationParticipatingOrganisations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModificationParticipatingOrganisations_ProjectModificationChanges_ProjectModificationChangeId",
                        column: x => x.ProjectModificationChangeId,
                        principalTable: "ProjectModificationChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModificationParticipatingOrganisations_ProjectPersonnels_ProjectPersonnelId",
                        column: x => x.ProjectPersonnelId,
                        principalTable: "ProjectPersonnels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModificationParticipatingOrganisations_ProjectRecords_ProjectRecordId",
                        column: x => x.ProjectRecordId,
                        principalTable: "ProjectRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ModificationDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectModificationChangeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectPersonnelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectRecordId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DocumentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false),
                    SponsorDocumentVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasPreviousVersion = table.Column<bool>(type: "bit", nullable: false),
                    SponsorDocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificationDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModificationDocuments_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModificationDocuments_ProjectModificationChanges_ProjectModificationChangeId",
                        column: x => x.ProjectModificationChangeId,
                        principalTable: "ProjectModificationChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModificationDocuments_ProjectPersonnels_ProjectPersonnelId",
                        column: x => x.ProjectPersonnelId,
                        principalTable: "ProjectPersonnels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModificationDocuments_ProjectRecords_ProjectRecordId",
                        column: x => x.ProjectRecordId,
                        principalTable: "ProjectRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ModificationParticipatingOrganisationAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModificationParticipatingOrganisationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VersionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectedOptions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificationParticipatingOrganisationAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModificationParticipatingOrganisationAnswers_ModificationParticipatingOrganisations_ModificationParticipatingOrganisationId",
                        column: x => x.ModificationParticipatingOrganisationId,
                        principalTable: "ModificationParticipatingOrganisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModificationDocuments_DocumentTypeId",
                table: "ModificationDocuments",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationDocuments_ProjectModificationChangeId",
                table: "ModificationDocuments",
                column: "ProjectModificationChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationDocuments_ProjectPersonnelId",
                table: "ModificationDocuments",
                column: "ProjectPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationDocuments_ProjectRecordId",
                table: "ModificationDocuments",
                column: "ProjectRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationParticipatingOrganisationAnswers_ModificationParticipatingOrganisationId",
                table: "ModificationParticipatingOrganisationAnswers",
                column: "ModificationParticipatingOrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationParticipatingOrganisations_ProjectModificationChangeId",
                table: "ModificationParticipatingOrganisations",
                column: "ProjectModificationChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationParticipatingOrganisations_ProjectPersonnelId",
                table: "ModificationParticipatingOrganisations",
                column: "ProjectPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationParticipatingOrganisations_ProjectRecordId",
                table: "ModificationParticipatingOrganisations",
                column: "ProjectRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModificationDocuments");

            migrationBuilder.DropTable(
                name: "ModificationParticipatingOrganisationAnswers");

            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropTable(
                name: "ModificationParticipatingOrganisations");

            migrationBuilder.DropColumn(
                name: "VersionId",
                table: "ProjectRecordAnswers");

            migrationBuilder.DropColumn(
                name: "VersionId",
                table: "ProjectModificationAnswers");
        }
    }
}
