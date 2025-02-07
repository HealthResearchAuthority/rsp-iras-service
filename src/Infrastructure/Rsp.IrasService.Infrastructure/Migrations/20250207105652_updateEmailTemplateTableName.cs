using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateEmailTemplateTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailTemplated_EventTypes_EventTypeId",
                table: "EmailTemplated");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailTemplated",
                table: "EmailTemplated");

            migrationBuilder.RenameTable(
                name: "EmailTemplated",
                newName: "EmailTemplates");

            migrationBuilder.RenameIndex(
                name: "IX_EmailTemplated_EventTypeId",
                table: "EmailTemplates",
                newName: "IX_EmailTemplates_EventTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailTemplates",
                table: "EmailTemplates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailTemplates_EventTypes_EventTypeId",
                table: "EmailTemplates",
                column: "EventTypeId",
                principalTable: "EventTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailTemplates_EventTypes_EventTypeId",
                table: "EmailTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailTemplates",
                table: "EmailTemplates");

            migrationBuilder.RenameTable(
                name: "EmailTemplates",
                newName: "EmailTemplated");

            migrationBuilder.RenameIndex(
                name: "IX_EmailTemplates_EventTypeId",
                table: "EmailTemplated",
                newName: "IX_EmailTemplated_EventTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailTemplated",
                table: "EmailTemplated",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailTemplated_EventTypes_EventTypeId",
                table: "EmailTemplated",
                column: "EventTypeId",
                principalTable: "EventTypes",
                principalColumn: "Id");
        }
    }
}
