using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddModificationReviewValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProvisionalReviewOutcome",
                table: "ProjectModifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonNotApproved",
                table: "ProjectModifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewerComments",
                table: "ProjectModifications",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProvisionalReviewOutcome",
                table: "ProjectModifications");

            migrationBuilder.DropColumn(
                name: "ReasonNotApproved",
                table: "ProjectModifications");

            migrationBuilder.DropColumn(
                name: "ReviewerComments",
                table: "ProjectModifications");
        }
    }
}
