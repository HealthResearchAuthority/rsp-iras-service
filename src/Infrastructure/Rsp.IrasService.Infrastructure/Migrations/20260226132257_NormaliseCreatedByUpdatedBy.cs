using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NormaliseCreatedByUpdatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // I'm pretty sure that this won't cause any data loss issues but need to confirm
            // It is being recreated at the end of the migration using the new CreatedBy column instaed of UserId
            migrationBuilder.DropIndex(
                name: "IX_ProjectModificationChangeAnswers_ProjectRecordId_QuestionId_ProjectModificationChangeId",
                table: "ProjectModificationChangeAnswers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProjectRecords");

            // In order to preserve the data in the UserId column, we are renaming it to CreatedBy instead of deleting it.
            // We also need to alter the column to be non-nullable as the UserId column is currently nullable but CreatedBy should not be nullable.
            // Before we do this, we need to delete the existing CreatedBy column which is currently unused.

            // --- Automated migration ---
            //migrationBuilder.DropColumn(
            //    name: "UserId",
            //    table: "ProjectClosures");
            // --- Automated migration ---

            // --- Manual migration ---
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProjectClosures");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ProjectClosures",
                newName: "CreatedBy");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ProjectClosures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
            // --- Manual migration ---

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

            migrationBuilder.RenameIndex(
                name: "IX_ProjectRecordAnswers_ProjectRecordId_QuestionId_UserId",
                table: "ProjectRecordAnswers",
                newName: "IX_ProjectRecordAnswers_ProjectRecordId_QuestionId_CreatedBy");

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
                table: "ModificationParticipatingOrganisations",
                newName: "UpdatedBy");

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

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ProjectModificationChangeAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
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
                name: "UpdatedBy",
                table: "ProjectModificationAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "ProjectModificationAnswers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectClosures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "ProjectClosures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "ModificationRfiReasons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ModificationRfiReasons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ModificationParticipatingOrganisations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ModificationParticipatingOrganisations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "ModificationParticipatingOrganisations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ModificationParticipatingOrganisationAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ModificationParticipatingOrganisationAnswers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ModificationParticipatingOrganisationAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "ModificationParticipatingOrganisationAnswers",
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

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationChangeAnswers_ProjectRecordId_QuestionId_ProjectModificationChangeId",
                table: "ProjectModificationChangeAnswers",
                columns: new[] { "ProjectRecordId", "QuestionId", "ProjectModificationChangeId" })
                .Annotation("SqlServer:Include", new[] { "Response", "SelectedOptions", "OptionType", "CreatedBy" });

            migrationBuilder.Sql
            (@"
                ALTER VIEW dbo.vw_EffectiveProjectRecordAnswers
                AS
                WITH LatestApprovedModificationAnswers AS
                (
                    SELECT
                        pmca.ProjectRecordId,
                        pmca.QuestionId,
                        pmca.CreatedBy,
                        pmca.Response,
                        pmca.SelectedOptions,
                        pmca.OptionType,
                        pmc.CreatedDate,
                        ROW_NUMBER() OVER
                        (
                            PARTITION BY
                                pmca.ProjectRecordId,
                                pmca.QuestionId,
                                pmca.CreatedBy
                            ORDER BY
                                pm.CreatedDate DESC,
                                pmc.CreatedDate DESC
                        ) AS rn
                    FROM
                        ProjectModificationChangeAnswers pmca
                    INNER JOIN ProjectModificationChanges pmc
                        ON pmc.Id = pmca.ProjectModificationChangeId
                    INNER JOIN ProjectModifications pm
                        ON pm.Id = pmc.ProjectModificationId
                    WHERE
                        pm.Status = 'Approved'
                )
                SELECT
                    pra.ProjectRecordId,
                    pra.QuestionId,
                    pra.CreatedBy,
                    pra.CreatedDate,
                    pra.UpdatedBy,
                    pra.UpdatedDate,
                    pra.Category,
                    pra.Section,
                    pra.VersionId,
                    COALESCE(lama.Response, pra.Response) AS Response,
                    COALESCE(lama.SelectedOptions, pra.SelectedOptions) AS SelectedOptions,
                    COALESCE(lama.OptionType, pra.OptionType) AS OptionType,
                    CASE WHEN lama.QuestionId IS NOT NULL THEN 1 ELSE 0 END AS IsModified
                FROM dbo.ProjectRecordAnswers pra
                LEFT JOIN LatestApprovedModificationAnswers lama
                    ON lama.ProjectRecordId = pra.ProjectRecordId
                    AND lama.QuestionId = pra.QuestionId
                    AND lama.CreatedBy = pra.CreatedBy
                    AND lama.rn = 1;"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProjectModificationChangeAnswers_ProjectRecordId_QuestionId_ProjectModificationChangeId",
                table: "ProjectModificationChangeAnswers");

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
                name: "UpdatedBy",
                table: "ProjectModificationChangeAnswers");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ProjectModificationChangeAnswers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProjectModificationAnswers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ProjectModificationAnswers");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ProjectModificationAnswers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProjectClosures");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ProjectClosures");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ModificationParticipatingOrganisations");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ModificationParticipatingOrganisations");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ModificationParticipatingOrganisations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ModificationParticipatingOrganisationAnswers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ModificationParticipatingOrganisationAnswers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ModificationParticipatingOrganisationAnswers");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ModificationParticipatingOrganisationAnswers");

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

            migrationBuilder.RenameIndex(
                name: "IX_ProjectRecordAnswers_ProjectRecordId_QuestionId_CreatedBy",
                table: "ProjectRecordAnswers",
                newName: "IX_ProjectRecordAnswers_ProjectRecordId_QuestionId_UserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "ProjectModificationChangeAnswers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "ProjectModificationAnswers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "ModificationParticipatingOrganisations",
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

            // --- Automated migration ---
            //migrationBuilder.AddColumn<string>(
            //    name: "UserId",
            //    table: "ProjectClosures",
            //    type: "nvarchar(max)",
            //    nullable: true);
            // --- Automated migration ---

            // --- Manual migration ---
            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ProjectClosures",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "ProjectClosures",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProjectClosures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
            // --- Manual migration ---

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "ModificationRfiReasons",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "ModificationRfiReasons",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationChangeAnswers_ProjectRecordId_QuestionId_ProjectModificationChangeId",
                table: "ProjectModificationChangeAnswers",
                columns: new[] { "ProjectRecordId", "QuestionId", "ProjectModificationChangeId" })
                .Annotation("SqlServer:Include", new[] { "Response", "SelectedOptions", "OptionType", "UserId" });

            migrationBuilder.Sql
            (@"
                ALTER VIEW dbo.vw_EffectiveProjectRecordAnswers
                AS
                WITH LatestApprovedModificationAnswers AS
                (
                    SELECT
                        pmca.ProjectRecordId,
                        pmca.QuestionId,
                        pmca.UserId,
                        pmca.Response,
                        pmca.SelectedOptions,
                        pmca.OptionType,
                        pmc.CreatedDate,
                        ROW_NUMBER() OVER
                        (
                            PARTITION BY
                                pmca.ProjectRecordId,
                                pmca.QuestionId,
                                pmca.UserId
                            ORDER BY
                                pm.CreatedDate DESC,
                                pmc.CreatedDate DESC
                        ) AS rn
                    FROM
                        ProjectModificationChangeAnswers pmca
                    INNER JOIN ProjectModificationChanges pmc
                        ON pmc.Id = pmca.ProjectModificationChangeId
                    INNER JOIN ProjectModifications pm
                        ON pm.Id = pmc.ProjectModificationId
                    WHERE
                        pm.Status = 'Approved'
                )
                SELECT
                    pra.ProjectRecordId,
                    pra.QuestionId,
                    pra.UserId,
                    pra.Category,
                    pra.Section,
                    pra.VersionId,
                    COALESCE(lama.Response, pra.Response) AS Response,
                    COALESCE(lama.SelectedOptions, pra.SelectedOptions) AS SelectedOptions,
                    COALESCE(lama.OptionType, pra.OptionType) AS OptionType,
                    CASE WHEN lama.QuestionId IS NOT NULL THEN 1 ELSE 0 END AS IsModified
                FROM dbo.ProjectRecordAnswers pra
                LEFT JOIN LatestApprovedModificationAnswers lama
                    ON lama.ProjectRecordId = pra.ProjectRecordId
                    AND lama.QuestionId = pra.QuestionId
                    AND lama.UserId = pra.UserId
                    AND lama.rn = 1;"
            );
        }
    }
}