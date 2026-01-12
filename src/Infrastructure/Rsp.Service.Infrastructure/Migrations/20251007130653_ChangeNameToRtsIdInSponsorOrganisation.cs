using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameToRtsIdInSponsorOrganisation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Countries",
                table: "SponsorOrganisations");

            migrationBuilder.DropColumn(
                name: "SponsorOrganisationName",
                table: "SponsorOrganisations");

            migrationBuilder.AddColumn<string>(
                name: "RtsId",
                table: "SponsorOrganisations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RtsId",
                table: "SponsorOrganisations");

            migrationBuilder.AddColumn<string>(
                name: "Countries",
                table: "SponsorOrganisations",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SponsorOrganisationName",
                table: "SponsorOrganisations",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }
    }
}
