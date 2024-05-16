using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "IrasApplications",
                columns: new[] { "Id", "ApplicationCategories", "Location", "ProjectCategory", "StartDate", "Title" },
                values: new object[,]
                {
                    { 1, "[\"Application category 1\",\"Application category 2\"]", 0, "Project category 1", new DateTime(2024, 5, 20, 15, 37, 16, 619, DateTimeKind.Local).AddTicks(2007), "Example 1" },
                    { 2, "[\"Application category 1\",\"Application category 2\"]", 1, "Project category 2", new DateTime(2024, 5, 20, 15, 37, 16, 619, DateTimeKind.Local).AddTicks(2085), "Example 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IrasApplications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IrasApplications",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
