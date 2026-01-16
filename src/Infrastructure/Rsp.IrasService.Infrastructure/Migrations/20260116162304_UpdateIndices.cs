using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIndices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProjectRecordAnswers_ProjectRecordId",
                table: "ProjectRecordAnswers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectModifications_ProjectRecordId",
                table: "ProjectModifications");

            migrationBuilder.DropIndex(
                name: "IX_ProjectModificationChanges_ProjectModificationId",
                table: "ProjectModificationChanges");

            migrationBuilder.DropIndex(
                name: "IX_ProjectModificationChangeAnswers_ProjectRecordId",
                table: "ProjectModificationChangeAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRecordAnswers_ProjectRecordId_QuestionId_UserId",
                table: "ProjectRecordAnswers",
                columns: new[] { "ProjectRecordId", "QuestionId", "UserId" })
                .Annotation("SqlServer:Include", new[] { "Response", "SelectedOptions", "OptionType", "Category", "Section", "VersionId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModifications_ProjectRecordId_CreatedDate",
                table: "ProjectModifications",
                columns: new[] { "ProjectRecordId", "CreatedDate" },
                descending: new[] { false, true })
                .Annotation("SqlServer:Include", new[] { "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationChanges_ProjectModificationId_CreatedDate",
                table: "ProjectModificationChanges",
                columns: new[] { "ProjectModificationId", "CreatedDate" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationChangeAnswers_ProjectRecordId_QuestionId_ProjectModificationChangeId",
                table: "ProjectModificationChangeAnswers",
                columns: new[] { "ProjectRecordId", "QuestionId", "ProjectModificationChangeId" })
                .Annotation("SqlServer:Include", new[] { "Response", "SelectedOptions", "OptionType", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProjectRecordAnswers_ProjectRecordId_QuestionId_UserId",
                table: "ProjectRecordAnswers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectModifications_ProjectRecordId_CreatedDate",
                table: "ProjectModifications");

            migrationBuilder.DropIndex(
                name: "IX_ProjectModificationChanges_ProjectModificationId_CreatedDate",
                table: "ProjectModificationChanges");

            migrationBuilder.DropIndex(
                name: "IX_ProjectModificationChangeAnswers_ProjectRecordId_QuestionId_ProjectModificationChangeId",
                table: "ProjectModificationChangeAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRecordAnswers_ProjectRecordId",
                table: "ProjectRecordAnswers",
                column: "ProjectRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModifications_ProjectRecordId",
                table: "ProjectModifications",
                column: "ProjectRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationChanges_ProjectModificationId",
                table: "ProjectModificationChanges",
                column: "ProjectModificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationChangeAnswers_ProjectRecordId",
                table: "ProjectModificationChangeAnswers",
                column: "ProjectRecordId");
        }
    }
}