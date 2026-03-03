using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDuplicateFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DuplicatedFromModificationIdentifier",
                table: "ProjectModifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDuplicate",
                table: "ProjectModifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDuplicate",
                table: "ProjectModificationChanges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDuplicate",
                table: "ProjectModificationChangeAnswers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDuplicate",
                table: "ProjectModificationAnswers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDuplicate",
                table: "ModificationDocuments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDuplicate",
                table: "ModificationDocumentAnswers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DuplicatedFromModificationIdentifier",
                table: "ProjectModifications");

            migrationBuilder.DropColumn(
                name: "IsDuplicate",
                table: "ProjectModifications");

            migrationBuilder.DropColumn(
                name: "IsDuplicate",
                table: "ProjectModificationChanges");

            migrationBuilder.DropColumn(
                name: "IsDuplicate",
                table: "ProjectModificationChangeAnswers");

            migrationBuilder.DropColumn(
                name: "IsDuplicate",
                table: "ProjectModificationAnswers");

            migrationBuilder.DropColumn(
                name: "IsDuplicate",
                table: "ModificationDocuments");

            migrationBuilder.DropColumn(
                name: "IsDuplicate",
                table: "ModificationDocumentAnswers");
        }
    }
}
