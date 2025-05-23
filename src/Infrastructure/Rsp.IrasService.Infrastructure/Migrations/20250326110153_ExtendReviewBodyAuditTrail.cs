using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExtendReviewBodyAuditTrail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReviewBodyId",
                table: "ReviewBodiesAuditTrail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ReviewBodiesAuditTrail_ReviewBodyId",
                table: "ReviewBodiesAuditTrail",
                column: "ReviewBodyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewBodiesAuditTrail_ReviewBodies_ReviewBodyId",
                table: "ReviewBodiesAuditTrail",
                column: "ReviewBodyId",
                principalTable: "ReviewBodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewBodiesAuditTrail_ReviewBodies_ReviewBodyId",
                table: "ReviewBodiesAuditTrail");

            migrationBuilder.DropIndex(
                name: "IX_ReviewBodiesAuditTrail_ReviewBodyId",
                table: "ReviewBodiesAuditTrail");

            migrationBuilder.DropColumn(
                name: "ReviewBodyId",
                table: "ReviewBodiesAuditTrail");
        }
    }
}
