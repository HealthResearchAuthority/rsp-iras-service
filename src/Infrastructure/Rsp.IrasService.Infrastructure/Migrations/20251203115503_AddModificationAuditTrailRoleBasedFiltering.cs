using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddModificationAuditTrailRoleBasedFiltering : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBackstageOnly",
                table: "ProjectModificationAuditTrail",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowUserEmailToFrontstage",
                table: "ProjectModificationAuditTrail",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBackstageOnly",
                table: "ProjectModificationAuditTrail");

            migrationBuilder.DropColumn(
                name: "ShowUserEmailToFrontstage",
                table: "ProjectModificationAuditTrail");
        }
    }
}