using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModificationDocumentAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModificationDocuments_DocumentTypes_DocumentTypeId",
                table: "ModificationDocuments");

            migrationBuilder.DropIndex(
                name: "IX_ModificationDocuments_DocumentTypeId",
                table: "ModificationDocuments");

            migrationBuilder.DropColumn(
                name: "DocumentTypeId",
                table: "ModificationDocuments");

            migrationBuilder.DropColumn(
                name: "HasPreviousVersion",
                table: "ModificationDocuments");

            migrationBuilder.DropColumn(
                name: "SponsorDocumentDate",
                table: "ModificationDocuments");

            migrationBuilder.DropColumn(
                name: "SponsorDocumentVersion",
                table: "ModificationDocuments");

            migrationBuilder.CreateTable(
                name: "ModificationDocumentAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModificationDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_ModificationDocumentAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModificationDocumentAnswers_ModificationDocuments_ModificationDocumentId",
                        column: x => x.ModificationDocumentId,
                        principalTable: "ModificationDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModificationDocumentAnswers_ModificationDocumentId",
                table: "ModificationDocumentAnswers",
                column: "ModificationDocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModificationDocumentAnswers");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentTypeId",
                table: "ModificationDocuments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasPreviousVersion",
                table: "ModificationDocuments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SponsorDocumentDate",
                table: "ModificationDocuments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SponsorDocumentVersion",
                table: "ModificationDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModificationDocuments_DocumentTypeId",
                table: "ModificationDocuments",
                column: "DocumentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModificationDocuments_DocumentTypes_DocumentTypeId",
                table: "ModificationDocuments",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
