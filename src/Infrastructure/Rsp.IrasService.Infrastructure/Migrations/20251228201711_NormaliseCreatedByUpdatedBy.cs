using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NormaliseCreatedByUpdatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProjectRecords");

            migrationBuilder.DropColumn(
                name: "ModfifiedAt",
                table: "EmailTemplates");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "EmailTemplates");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ProjectRecordAnswers",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ProjectModificationChangeAnswers",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ProjectModificationAnswers",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ModificationDocuments",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "EventTypes",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "EmailTemplates",
                newName: "UpdatedDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "SponsorOrganisations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "SponsorOrganisations",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "RegulatoryBodies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "RegulatoryBodies",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectRecordAnswers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ProjectRecordAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "ProjectRecordAnswers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectModificationChangeAnswers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectModificationAnswers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ModificationDocumentAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ModificationDocumentAnswers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ModificationDocumentAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "ModificationDocumentAnswers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "EmailTemplates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "EmailTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProjectRecordAnswers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ProjectRecordAnswers");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ProjectRecordAnswers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProjectModificationChangeAnswers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProjectModificationAnswers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ModificationDocumentAnswers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ModificationDocumentAnswers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ModificationDocumentAnswers");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ModificationDocumentAnswers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "EmailTemplates");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "EmailTemplates");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "ProjectRecordAnswers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "ProjectModificationChangeAnswers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "ProjectModificationAnswers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "ModificationDocuments",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "EventTypes",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "EmailTemplates",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "SponsorOrganisations",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "SponsorOrganisations",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "RegulatoryBodies",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "RegulatoryBodies",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProjectRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModfifiedAt",
                table: "EmailTemplates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "EmailTemplates",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
