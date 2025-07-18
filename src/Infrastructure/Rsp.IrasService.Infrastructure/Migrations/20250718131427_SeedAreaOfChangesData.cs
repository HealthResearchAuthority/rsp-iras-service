using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAreaOfChangesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seed data for ModificationAreaOfChanges
            migrationBuilder.InsertData(
                table: "ModificationAreaOfChanges",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
            { 1, "Participating organisations" },
            { 2, "Project design" }
                });

            // Seed data for ModificationSpecificAreaOfChanges
            migrationBuilder.InsertData(
                table: "ModificationSpecificAreaOfChanges",
                columns: new[] { "Id", "Name", "JourneyType", "ModificationAreaOfChangeId" },
                values: new object[,]
                {
            { 1, "Early closure or withdrawal of research sites", "Participating organisation", 1 },
            { 2, "PICs - Addition of Participant Identification Centres undertaking the same activities as existing PICs", "Participating organisation", 1 },
            { 3, "PICs - Early closure or withdrawal of Participant Identification Centres", "Participating organisation", 1 },
            { 4, "Change to planned end date", "Planned end date", 2 },
            { 5, "Addition of sites undertaking the same activities as existing sites", "Participating organisation", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5 });

            migrationBuilder.DeleteData(
                table: "ModificationAreaOfChanges",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2 });
        }
    }
}