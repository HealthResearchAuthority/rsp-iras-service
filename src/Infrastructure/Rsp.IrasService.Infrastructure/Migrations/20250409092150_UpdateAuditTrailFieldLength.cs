using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuditTrailFieldLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewBodiesAuditTrail_ReviewBodies_ReviewBodyId",
                table: "ReviewBodiesAuditTrail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewBodiesAuditTrail",
                table: "ReviewBodiesAuditTrail");

            migrationBuilder.RenameTable(
                name: "ReviewBodiesAuditTrail",
                newName: "ReviewBodiesAuditTrails");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewBodiesAuditTrail_ReviewBodyId",
                table: "ReviewBodiesAuditTrails",
                newName: "IX_ReviewBodiesAuditTrails_ReviewBodyId");

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "ReviewBodies",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "ReviewBodiesAuditTrails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewBodiesAuditTrails",
                table: "ReviewBodiesAuditTrails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewBodiesAuditTrails_ReviewBodies_ReviewBodyId",
                table: "ReviewBodiesAuditTrails",
                column: "ReviewBodyId",
                principalTable: "ReviewBodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewBodiesAuditTrails_ReviewBodies_ReviewBodyId",
                table: "ReviewBodiesAuditTrails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewBodiesAuditTrails",
                table: "ReviewBodiesAuditTrails");

            migrationBuilder.RenameTable(
                name: "ReviewBodiesAuditTrails",
                newName: "ReviewBodiesAuditTrail");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewBodiesAuditTrails_ReviewBodyId",
                table: "ReviewBodiesAuditTrail",
                newName: "IX_ReviewBodiesAuditTrail_ReviewBodyId");

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "ReviewBodies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "ReviewBodiesAuditTrail",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewBodiesAuditTrail",
                table: "ReviewBodiesAuditTrail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewBodiesAuditTrail_ReviewBodies_ReviewBodyId",
                table: "ReviewBodiesAuditTrail",
                column: "ReviewBodyId",
                principalTable: "ReviewBodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
