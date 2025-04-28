using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateKeyReviewBodyUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewBodyUsers",
                table: "ReviewBodyUsers");

            migrationBuilder.DropIndex(
                name: "IX_ReviewBodyUsers_ReviewBodyId",
                table: "ReviewBodyUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReviewBodyUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewBodyUsers",
                table: "ReviewBodyUsers",
                columns: new[] { "ReviewBodyId", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewBodyUsers",
                table: "ReviewBodyUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ReviewBodyUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewBodyUsers",
                table: "ReviewBodyUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewBodyUsers_ReviewBodyId",
                table: "ReviewBodyUsers",
                column: "ReviewBodyId");
        }
    }
}
