using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAreaOfChangesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ModificationAreaOfChanges",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Participating organisations" },
                    { 2, "Project design" }
                });

            migrationBuilder.InsertData(
                table: "ModificationSpecificAreaOfChanges",
                columns: new[] { "Id", "JourneyType", "ModificationAreaOfChangeId", "Name" },
                values: new object[,]
                {
                    { 1, "Participating organisation", 1, "Early closure or withdrawal of research sites" },
                    { 2, "Participating organisation", 1, "PICs - Addition of Participant Identification Centres undertaking the same activities as existing PICs" },
                    { 3, "Participating organisation", 1, "PICs - Early closure or withdrawal of Participant Identification Centres" },
                    { 4, "Planned end date", 2, "Change to planned end date" },
                    { 5, "Participating organisation", 1, "Addition of sites undertaking the same activities as existing sites" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ModificationSpecificAreaOfChanges",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ModificationAreaOfChanges",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ModificationAreaOfChanges",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
