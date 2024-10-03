using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResearchApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RespondentAnswers_Respondents_RespondentId",
                table: "RespondentAnswers");

            migrationBuilder.AddColumn<string>(
                name: "RespondentId",
                table: "ResearchApplications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchApplications_RespondentId",
                table: "ResearchApplications",
                column: "RespondentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResearchApplications_Respondents_RespondentId",
                table: "ResearchApplications",
                column: "RespondentId",
                principalTable: "Respondents",
                principalColumn: "RespondentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RespondentAnswers_Respondents_RespondentId",
                table: "RespondentAnswers",
                column: "RespondentId",
                principalTable: "Respondents",
                principalColumn: "RespondentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResearchApplications_Respondents_RespondentId",
                table: "ResearchApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_RespondentAnswers_Respondents_RespondentId",
                table: "RespondentAnswers");

            migrationBuilder.DropIndex(
                name: "IX_ResearchApplications_RespondentId",
                table: "ResearchApplications");

            migrationBuilder.DropColumn(
                name: "RespondentId",
                table: "ResearchApplications");

            migrationBuilder.AddForeignKey(
                name: "FK_RespondentAnswers_Respondents_RespondentId",
                table: "RespondentAnswers",
                column: "RespondentId",
                principalTable: "Respondents",
                principalColumn: "RespondentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
