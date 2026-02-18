using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SupersedeDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentType",
                table: "ModificationDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LinkedDocumentId",
                table: "ModificationDocuments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReplacedByDocumentId",
                table: "ModificationDocuments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReplacesDocumentId",
                table: "ModificationDocuments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModificationDocuments_LinkedDocumentId",
                table: "ModificationDocuments",
                column: "LinkedDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationDocuments_ReplacedByDocumentId",
                table: "ModificationDocuments",
                column: "ReplacedByDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModificationDocuments_ModificationDocuments_LinkedDocumentId",
                table: "ModificationDocuments",
                column: "LinkedDocumentId",
                principalTable: "ModificationDocuments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModificationDocuments_ModificationDocuments_ReplacedByDocumentId",
                table: "ModificationDocuments",
                column: "ReplacedByDocumentId",
                principalTable: "ModificationDocuments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModificationDocuments_ModificationDocuments_LinkedDocumentId",
                table: "ModificationDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_ModificationDocuments_ModificationDocuments_ReplacedByDocumentId",
                table: "ModificationDocuments");

            migrationBuilder.DropIndex(
                name: "IX_ModificationDocuments_LinkedDocumentId",
                table: "ModificationDocuments");

            migrationBuilder.DropIndex(
                name: "IX_ModificationDocuments_ReplacedByDocumentId",
                table: "ModificationDocuments");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "ModificationDocuments");

            migrationBuilder.DropColumn(
                name: "LinkedDocumentId",
                table: "ModificationDocuments");

            migrationBuilder.DropColumn(
                name: "ReplacedByDocumentId",
                table: "ModificationDocuments");

            migrationBuilder.DropColumn(
                name: "ReplacesDocumentId",
                table: "ModificationDocuments");
        }
    }
}
