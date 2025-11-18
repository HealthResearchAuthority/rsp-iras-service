using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveDocumentsToModificationLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [ModificationDocumentAnswers];");

            migrationBuilder.Sql("DELETE FROM [ModificationDocuments];");

            migrationBuilder.DropForeignKey(
                name: "FK_ModificationDocuments_ProjectModificationChanges_ProjectModificationChangeId",
                table: "ModificationDocuments");

            migrationBuilder.RenameColumn(
                name: "ProjectModificationChangeId",
                table: "ModificationDocuments",
                newName: "ProjectModificationId");

            migrationBuilder.RenameIndex(
                name: "IX_ModificationDocuments_ProjectModificationChangeId",
                table: "ModificationDocuments",
                newName: "IX_ModificationDocuments_ProjectModificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModificationDocuments_ProjectModifications_ProjectModificationId",
                table: "ModificationDocuments",
                column: "ProjectModificationId",
                principalTable: "ProjectModifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModificationDocuments_ProjectModifications_ProjectModificationId",
                table: "ModificationDocuments");

            migrationBuilder.RenameColumn(
                name: "ProjectModificationId",
                table: "ModificationDocuments",
                newName: "ProjectModificationChangeId");

            migrationBuilder.RenameIndex(
                name: "IX_ModificationDocuments_ProjectModificationId",
                table: "ModificationDocuments",
                newName: "IX_ModificationDocuments_ProjectModificationChangeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModificationDocuments_ProjectModificationChanges_ProjectModificationChangeId",
                table: "ModificationDocuments",
                column: "ProjectModificationChangeId",
                principalTable: "ProjectModificationChanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}