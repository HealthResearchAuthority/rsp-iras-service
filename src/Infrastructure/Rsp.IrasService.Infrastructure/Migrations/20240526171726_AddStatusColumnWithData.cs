using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusColumnWithData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "IrasApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "IrasApplications",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "StartDate", "Status" },
                values: new object[] { new DateTime(2024, 5, 26, 18, 17, 26, 124, DateTimeKind.Local).AddTicks(181), "pending" });

            migrationBuilder.UpdateData(
                table: "IrasApplications",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "StartDate", "Status" },
                values: new object[] { new DateTime(2024, 5, 26, 18, 17, 26, 124, DateTimeKind.Local).AddTicks(264), "approved" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "IrasApplications");

            migrationBuilder.UpdateData(
                table: "IrasApplications",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2024, 5, 20, 15, 37, 16, 619, DateTimeKind.Local).AddTicks(2007));

            migrationBuilder.UpdateData(
                table: "IrasApplications",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2024, 5, 20, 15, 37, 16, 619, DateTimeKind.Local).AddTicks(2085));
        }
    }
}
