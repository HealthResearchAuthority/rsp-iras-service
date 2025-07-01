using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlignWithBridgStandards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Rename the RespondentAnswers table to ProjectRecordAnswers
            migrationBuilder.RenameTable(
                name: "RespondentAnswers",
                newName: "ProjectRecordAnswers");

            migrationBuilder.DropForeignKey(
            name: "FK_RespondentAnswers_ResearchApplications_ApplicationId",
            table: "ProjectRecordAnswers");

            migrationBuilder.DropForeignKey(
            name: "FK_RespondentAnswers_Respondents_RespondentId",
            table: "ProjectRecordAnswers");

            migrationBuilder.DropPrimaryKey(
            name: "PK_RespondentAnswers",
            table: "ProjectRecordAnswers");

            migrationBuilder.DropColumn(
            name: "StartDate",
            table: "ProjectRecordAnswers");

            migrationBuilder.DropColumn(
            name: "EndDate",
            table: "ProjectRecordAnswers");

            migrationBuilder.DropColumn(
            name: "Version",
            table: "ProjectRecordAnswers");

            migrationBuilder.RenameColumn(
            name: "RespondentId",
            table: "ProjectRecordAnswers",
            newName: "ProjectPersonnelId");

            migrationBuilder.RenameColumn(
            name: "ApplicationId",
            table: "ProjectRecordAnswers",
            newName: "ProjectRecordId");

            migrationBuilder.AddPrimaryKey(
            name: "PK_ProjectRecordAnswers",
            table: "ProjectRecordAnswers",
            columns: ["ProjectPersonnelId", "QuestionId", "ProjectRecordId"]);

            // Rename the ReviewBodiesAuditTrails table to RegulatoryBodiesAuditTrail
            migrationBuilder.RenameTable(
                name: "ReviewBodiesAuditTrails",
                newName: "RegulatoryBodiesAuditTrail");

            migrationBuilder.DropForeignKey(
            name: "FK_ReviewBodiesAuditTrails_ReviewBodies_ReviewBodyId",
            table: "RegulatoryBodiesAuditTrail");

            migrationBuilder.DropPrimaryKey(
            name: "PK_ReviewBodiesAuditTrails",
            table: "RegulatoryBodiesAuditTrail");

            migrationBuilder.RenameColumn(
            name: "ReviewBodyId",
            table: "RegulatoryBodiesAuditTrail",
            newName: "RegulatoryBodyId");

            migrationBuilder.AddPrimaryKey(
            name: "PK_RegulatoryBodiesAuditTrail",
            table: "RegulatoryBodiesAuditTrail",
            column: "Id");

            // Rename the ReviewBodyUsers table to RegulatoryBodiesUsers
            migrationBuilder.RenameTable(
                name: "ReviewBodyUsers",
                newName: "RegulatoryBodiesUsers");

            migrationBuilder.DropForeignKey(
            name: "FK_ReviewBodyUsers_ReviewBodies_ReviewBodyId",
            table: "RegulatoryBodiesUsers");

            migrationBuilder.DropPrimaryKey(
            name: "PK_ReviewBodyUsers",
            table: "RegulatoryBodiesUsers");

            migrationBuilder.RenameColumn(
            name: "ReviewBodyId",
            table: "RegulatoryBodiesUsers",
            newName: "Id");

            migrationBuilder.AddPrimaryKey(
            name: "PK_RegulatoryBodiesUsers",
            table: "RegulatoryBodiesUsers",
            columns: ["Id", "UserId"]);

            // Rename the ResearchApplications table to ProjectRecords
            migrationBuilder.RenameTable(
                name: "ResearchApplications",
                newName: "ProjectRecords");

            migrationBuilder.DropForeignKey(
            name: "FK_ResearchApplications_Respondents_RespondentId",
            table: "ProjectRecords");

            migrationBuilder.DropPrimaryKey(
            name: "PK_ResearchApplications",
            table: "ProjectRecords");

            migrationBuilder.RenameColumn(
            name: "ApplicationId",
            table: "ProjectRecords",
            newName: "Id");

            migrationBuilder.RenameColumn(
            name: "RespondentId",
            table: "ProjectRecords",
            newName: "ProjectPersonnelId");

            migrationBuilder.AddPrimaryKey(
            name: "PK_ProjectRecords",
            table: "ProjectRecords",
            column: "Id");

            // Rename the ReviewBodies table to RegulatoryBodies
            migrationBuilder.RenameTable(
                name: "ReviewBodies",
                newName: "RegulatoryBodies");

            migrationBuilder.DropPrimaryKey(
            name: "PK_ReviewBodies",
            table: "RegulatoryBodies");

            migrationBuilder.RenameColumn(
            name: "OrganisationName",
            table: "RegulatoryBodies",
            newName: "RegulatoryBodyName");

            migrationBuilder.AddPrimaryKey(
            name: "PK_RegulatoryBodies",
            table: "RegulatoryBodies",
            column: "Id");

            // Rename the Respondents table to ProjectPersonnels
            migrationBuilder.RenameTable(
                name: "Respondents",
                newName: "ProjectPersonnels");

            migrationBuilder.DropColumn(
            name: "StartDate",
            table: "ProjectPersonnels");

            migrationBuilder.DropColumn(
            name: "EndDate",
            table: "ProjectPersonnels");

            migrationBuilder.DropColumn(
            name: "Version",
            table: "ProjectPersonnels");

            migrationBuilder.RenameColumn(
            name: "RespondentId",
            table: "ProjectPersonnels",
            newName: "Id");

            migrationBuilder.RenameColumn(
            name: "FirstName",
            table: "ProjectPersonnels",
            newName: "GivenName");

            migrationBuilder.RenameColumn(
            name: "LastName",
            table: "ProjectPersonnels",
            newName: "FamilyName");

            // Update foreign keys to match the new table and column names
            migrationBuilder.AddForeignKey(
            name: "FK_ProjectRecords_ProjectPersonnels_ProjectPersonnelId",
            table: "ProjectRecords",
            column: "ProjectPersonnelId",
            principalTable: "ProjectPersonnels",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegulatoryBodiesUsers_RegulatoryBodies_Id",
                table: "RegulatoryBodiesUsers",
                column: "Id",
                principalTable: "RegulatoryBodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
            name: "FK_RegulatoryBodiesAuditTrail_RegulatoryBodies_RegulatoryBodyId",
            table: "RegulatoryBodiesAuditTrail",
            column: "RegulatoryBodyId",
            principalTable: "RegulatoryBodies",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
            name: "FK_ProjectRecordAnswers_ProjectPersonnels_ProjectPersonnelId",
            table: "ProjectRecordAnswers",
            column: "ProjectPersonnelId",
            principalTable: "ProjectPersonnels",
            principalColumn: "Id");

            migrationBuilder.AddForeignKey(
            name: "FK_ProjectRecordAnswers_ProjectRecords_ProjectRecordId",
            table: "ProjectRecordAnswers",
            column: "ProjectRecordId",
            principalTable: "ProjectRecords",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

            // Create indexes for the new foreign keys
            migrationBuilder.CreateIndex(
                name: "IX_ProjectRecordAnswers_ProjectRecordId",
                table: "ProjectRecordAnswers",
                column: "ProjectRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRecords_ProjectPersonnelId",
                table: "ProjectRecords",
                column: "ProjectPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_RegulatoryBodiesAuditTrail_RegulatoryBodyId",
                table: "RegulatoryBodiesAuditTrail",
                column: "RegulatoryBodyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectRecordAnswers");

            migrationBuilder.DropTable(
                name: "RegulatoryBodiesAuditTrail");

            migrationBuilder.DropTable(
                name: "RegulatoryBodiesUsers");

            migrationBuilder.DropTable(
                name: "ProjectRecords");

            migrationBuilder.DropTable(
                name: "RegulatoryBodies");

            migrationBuilder.DropTable(
                name: "ProjectPersonnels");

            migrationBuilder.CreateTable(
                name: "Respondents",
                columns: table => new
                {
                    RespondentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respondents", x => x.RespondentId);
                });

            migrationBuilder.CreateTable(
                name: "ReviewBodies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Countries = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OrganisationName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewBodies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResearchApplications",
                columns: table => new
                {
                    ApplicationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IrasId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RespondentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchApplications", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_ResearchApplications_Respondents_RespondentId",
                        column: x => x.RespondentId,
                        principalTable: "Respondents",
                        principalColumn: "RespondentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewBodiesAuditTrails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewBodyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewBodiesAuditTrails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewBodiesAuditTrails_ReviewBodies_ReviewBodyId",
                        column: x => x.ReviewBodyId,
                        principalTable: "ReviewBodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewBodyUsers",
                columns: table => new
                {
                    ReviewBodyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewBodyUsers", x => new { x.ReviewBodyId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ReviewBodyUsers_ReviewBodies_ReviewBodyId",
                        column: x => x.ReviewBodyId,
                        principalTable: "ReviewBodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespondentAnswers",
                columns: table => new
                {
                    RespondentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuestionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OptionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelectedOptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespondentAnswers", x => new { x.RespondentId, x.QuestionId, x.ApplicationId });
                    table.ForeignKey(
                        name: "FK_RespondentAnswers_ResearchApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "ResearchApplications",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RespondentAnswers_Respondents_RespondentId",
                        column: x => x.RespondentId,
                        principalTable: "Respondents",
                        principalColumn: "RespondentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResearchApplications_RespondentId",
                table: "ResearchApplications",
                column: "RespondentId");

            migrationBuilder.CreateIndex(
                name: "IX_RespondentAnswers_ApplicationId",
                table: "RespondentAnswers",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewBodiesAuditTrails_ReviewBodyId",
                table: "ReviewBodiesAuditTrails",
                column: "ReviewBodyId");
        }
    }
}