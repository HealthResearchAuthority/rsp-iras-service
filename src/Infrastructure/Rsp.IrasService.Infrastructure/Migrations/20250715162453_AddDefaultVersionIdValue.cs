using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultVersionIdValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VersionId",
                table: "ProjectRecordAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "published version",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "VersionId",
                table: "ProjectModificationAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "published version",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "VersionId",
                table: "ModificationParticipatingOrganisationAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "published version",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VersionId",
                table: "ProjectRecordAnswers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "published version");

            migrationBuilder.AlterColumn<string>(
                name: "VersionId",
                table: "ProjectModificationAnswers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "published version");

            migrationBuilder.AlterColumn<string>(
                name: "VersionId",
                table: "ModificationParticipatingOrganisationAnswers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "published version");
        }
    }
}
