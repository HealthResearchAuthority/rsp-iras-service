using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedModificationProjectDocumentData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentStoragePath",
                table: "ModificationDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "ModificationAreaOfChanges",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Project documents" });

            migrationBuilder.UpdateData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 1,
                column: "JourneyType",
                value: "Participating organisations");

            migrationBuilder.UpdateData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 2,
                column: "JourneyType",
                value: "Participating organisations");

            migrationBuilder.UpdateData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 3,
                column: "JourneyType",
                value: "Participating organisations");

            migrationBuilder.UpdateData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 5,
                column: "JourneyType",
                value: "Participating organisations");

            migrationBuilder.InsertData(
                table: "ModificationSpecificAreaOfChanges",
                columns: new[] { "Id", "JourneyType", "ModificationAreaOfChangeId", "Name" },
                values: new object[,]
                {
                    { 6, "Project documents", 3, "Correction of typographical errors" },
                    { 7, "Project documents", 3, "CRF/other study data records - Changes in the documentation used by the research team for recording study data" },
                    { 8, "Project documents", 3, "GDPR wording - Accepted wording used verbatim" },
                    { 9, "Project documents", 3, "Other minor change to study documents (e.g. information sheets, consent forms, questionnaires, letters) that will have additional resource implications for participating organisations" },
                    { 10, "Project documents", 3, "Post trial information for participants (which does not contradict the protocol)" },
                    { 11, "Project documents", 3, "Protocol - Non-substantial changes (e.g. not affecting safety or the scientific value of the trial)" },
                    { 12, "Project documents", 3, "Translations - Addition of translated versions of participant-facing documentation" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ModificationAreaOfChanges",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "DocumentStoragePath",
                table: "ModificationDocuments");

            migrationBuilder.UpdateData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 1,
                column: "JourneyType",
                value: "Participating organisation");

            migrationBuilder.UpdateData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 2,
                column: "JourneyType",
                value: "Participating organisation");

            migrationBuilder.UpdateData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 3,
                column: "JourneyType",
                value: "Participating organisation");

            migrationBuilder.UpdateData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 5,
                column: "JourneyType",
                value: "Participating organisation");
        }
    }
}
