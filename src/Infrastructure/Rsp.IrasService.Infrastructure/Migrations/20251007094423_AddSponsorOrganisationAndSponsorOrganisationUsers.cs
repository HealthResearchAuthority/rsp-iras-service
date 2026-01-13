using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSponsorOrganisationAndSponsorOrganisationUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SponsorOrganisations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SponsorOrganisationName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Countries = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SponsorOrganisations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SponsorOrganisationsUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SponsorOrganisationsUsers", x => new { x.Id, x.UserId });
                    table.ForeignKey(
                        name: "FK_SponsorOrganisationsUsers_SponsorOrganisations_Id",
                        column: x => x.Id,
                        principalTable: "SponsorOrganisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SponsorOrganisationsUsers");

            migrationBuilder.DropTable(
                name: "SponsorOrganisations");
        }
    }
}
