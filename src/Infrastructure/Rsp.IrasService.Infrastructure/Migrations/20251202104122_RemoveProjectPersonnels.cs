using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProjectPersonnels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModificationDocuments_ProjectPersonnels_ProjectPersonnelId",
                table: "ModificationDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_ModificationParticipatingOrganisations_ProjectPersonnels_ProjectPersonnelId",
                table: "ModificationParticipatingOrganisations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectModificationAnswers_ProjectPersonnels_ProjectPersonnelId",
                table: "ProjectModificationAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectModificationChangeAnswers_ProjectPersonnels_ProjectPersonnelId",
                table: "ProjectModificationChangeAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRecordAnswers_ProjectPersonnels_ProjectPersonnelId",
                table: "ProjectRecordAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRecords_ProjectPersonnels_ProjectPersonnelId",
                table: "ProjectRecords");

            migrationBuilder.DropTable(
                name: "ProjectPersonnels");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRecords_ProjectPersonnelId",
                table: "ProjectRecords");

            migrationBuilder.DropIndex(
                name: "IX_ResearchApplications_RespondentId",
                table: "ProjectRecords");

            migrationBuilder.DropIndex(
                name: "IX_ProjectModificationChangeAnswers_ProjectPersonnelId",
                table: "ProjectModificationChangeAnswers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectModificationAnswers_ProjectPersonnelId",
                table: "ProjectModificationAnswers");

            migrationBuilder.DropIndex(
                name: "IX_ModificationParticipatingOrganisations_ProjectPersonnelId",
                table: "ModificationParticipatingOrganisations");

            migrationBuilder.DropIndex(
                name: "IX_ModificationDocuments_ProjectPersonnelId",
                table: "ModificationDocuments");

            migrationBuilder.DropColumn(
                name: "ProjectPersonnelId",
                table: "ProjectRecords");

            migrationBuilder.DropColumn(
                name: "ProjectPersonnelId",
                table: "ModificationParticipatingOrganisations");

            migrationBuilder.DropColumn(
                name: "ProjectPersonnelId",
                table: "ModificationDocuments");

            migrationBuilder.RenameColumn(
                name: "ProjectPersonnelId",
                table: "ProjectRecordAnswers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ProjectPersonnelId",
                table: "ProjectModificationChangeAnswers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ProjectPersonnelId",
                table: "ProjectModificationAnswers",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProjectRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ModificationParticipatingOrganisations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ModificationDocuments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProjectRecords");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ModificationParticipatingOrganisations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ModificationDocuments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ProjectRecordAnswers",
                newName: "ProjectPersonnelId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ProjectModificationChangeAnswers",
                newName: "ProjectPersonnelId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ProjectModificationAnswers",
                newName: "ProjectPersonnelId");

            migrationBuilder.AddColumn<string>(
                name: "ProjectPersonnelId",
                table: "ProjectRecords",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectPersonnelId",
                table: "ModificationParticipatingOrganisations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectPersonnelId",
                table: "ModificationDocuments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ProjectPersonnels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GivenName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPersonnels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRecords_ProjectPersonnelId",
                table: "ProjectRecords",
                column: "ProjectPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationChangeAnswers_ProjectPersonnelId",
                table: "ProjectModificationChangeAnswers",
                column: "ProjectPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationAnswers_ProjectPersonnelId",
                table: "ProjectModificationAnswers",
                column: "ProjectPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationParticipatingOrganisations_ProjectPersonnelId",
                table: "ModificationParticipatingOrganisations",
                column: "ProjectPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationDocuments_ProjectPersonnelId",
                table: "ModificationDocuments",
                column: "ProjectPersonnelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModificationDocuments_ProjectPersonnels_ProjectPersonnelId",
                table: "ModificationDocuments",
                column: "ProjectPersonnelId",
                principalTable: "ProjectPersonnels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModificationParticipatingOrganisations_ProjectPersonnels_ProjectPersonnelId",
                table: "ModificationParticipatingOrganisations",
                column: "ProjectPersonnelId",
                principalTable: "ProjectPersonnels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectModificationAnswers_ProjectPersonnels_ProjectPersonnelId",
                table: "ProjectModificationAnswers",
                column: "ProjectPersonnelId",
                principalTable: "ProjectPersonnels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectModificationChangeAnswers_ProjectPersonnels_ProjectPersonnelId",
                table: "ProjectModificationChangeAnswers",
                column: "ProjectPersonnelId",
                principalTable: "ProjectPersonnels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRecordAnswers_ProjectPersonnels_ProjectPersonnelId",
                table: "ProjectRecordAnswers",
                column: "ProjectPersonnelId",
                principalTable: "ProjectPersonnels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRecords_ProjectPersonnels_ProjectPersonnelId",
                table: "ProjectRecords",
                column: "ProjectPersonnelId",
                principalTable: "ProjectPersonnels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}