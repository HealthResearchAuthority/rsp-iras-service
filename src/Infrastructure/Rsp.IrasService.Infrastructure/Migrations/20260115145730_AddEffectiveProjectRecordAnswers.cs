using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEffectiveProjectRecordAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql
            (@"
                CREATE VIEW dbo.vw_EffectiveProjectRecordAnswers
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW dbo.vw_EffectiveProjectRecordAnswers;");
        }
    }
}