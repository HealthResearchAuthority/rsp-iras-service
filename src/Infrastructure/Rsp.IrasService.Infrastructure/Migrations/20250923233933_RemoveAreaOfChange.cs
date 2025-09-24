using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAreaOfChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModificationSpecificAreaOfChanges");

            migrationBuilder.DropTable(
                name: "ModificationAreaOfChanges");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModificationAreaOfChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificationAreaOfChanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModificationSpecificAreaOfChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JourneyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationAreaOfChangeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificationSpecificAreaOfChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModificationSpecificAreaOfChanges_ModificationAreaOfChanges_ModificationAreaOfChangeId",
                        column: x => x.ModificationAreaOfChangeId,
                        principalTable: "ModificationAreaOfChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ModificationAreaOfChanges",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Participating organisations" },
                    { 2, "Project design" },
                    { 3, "Project documents" }
                });

            migrationBuilder.InsertData(
                table: "ModificationSpecificAreaOfChanges",
                columns: new[] { "Id", "JourneyType", "ModificationAreaOfChangeId", "Name" },
                values: new object[,]
                {
                    { 1, "Participating organisations", 1, "Early closure or withdrawal of research sites" },
                    { 2, "Participating organisations", 1, "PICs - Addition of Participant Identification Centres undertaking the same activities as existing PICs" },
                    { 3, "Participating organisations", 1, "PICs - Early closure or withdrawal of Participant Identification Centres" },
                    { 4, "Planned end date", 2, "Change to planned end date" },
                    { 5, "Participating organisations", 1, "Addition of sites undertaking the same activities as existing sites" },
                    { 6, "Project documents", 3, "Correction of typographical errors" },
                    { 7, "Project documents", 3, "CRF/other study data records - Changes in the documentation used by the research team for recording study data" },
                    { 8, "Project documents", 3, "GDPR wording - Accepted wording used verbatim" },
                    { 9, "Project documents", 3, "Other minor change to study documents (e.g. information sheets, consent forms, questionnaires, letters) that will have additional resource implications for participating organisations" },
                    { 10, "Project documents", 3, "Post trial information for participants (which does not contradict the protocol)" },
                    { 11, "Project documents", 3, "Protocol - Non-substantial changes (e.g. not affecting safety or the scientific value of the trial)" },
                    { 12, "Project documents", 3, "Translations - Addition of translated versions of participant-facing documentation" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModificationSpecificAreaOfChanges_ModificationAreaOfChangeId",
                table: "ModificationSpecificAreaOfChanges",
                column: "ModificationAreaOfChangeId");
        }
    }
}