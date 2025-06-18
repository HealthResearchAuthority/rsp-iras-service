using System;
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
            migrationBuilder.DropTable(
                name: "RespondentAnswers");

            migrationBuilder.DropTable(
                name: "ReviewBodiesAuditTrails");

            migrationBuilder.DropTable(
                name: "ReviewBodyUsers");

            migrationBuilder.DropTable(
                name: "ResearchApplications");

            migrationBuilder.DropTable(
                name: "ReviewBodies");

            migrationBuilder.DropTable(
                name: "Respondents");

            migrationBuilder.CreateTable(
                name: "ProjectApplicationRespondents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GivenName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectApplicationRespondents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegulatoryBodies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegulatoryBodyName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Countries = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegulatoryBodies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectApplications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectApplicationRespondentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IrasId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectApplications_ProjectApplicationRespondents_ProjectApplicationRespondentId",
                        column: x => x.ProjectApplicationRespondentId,
                        principalTable: "ProjectApplicationRespondents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegulatoryBodyAuditTrial",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegulatoryBodiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegulatoryBodyAuditTrial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegulatoryBodyAuditTrial_RegulatoryBodies_RegulatoryBodiesId",
                        column: x => x.RegulatoryBodiesId,
                        principalTable: "RegulatoryBodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegulatoryBodyUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegulatoryBodyUsers", x => new { x.Id, x.UserId });
                    table.ForeignKey(
                        name: "FK_RegulatoryBodyUsers_RegulatoryBodies_Id",
                        column: x => x.Id,
                        principalTable: "RegulatoryBodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectApplicationRespondentAnswers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectApplicationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuestionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectedOptions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectApplicationRespondentAnswers", x => new { x.Id, x.QuestionId, x.ProjectApplicationId });
                    table.ForeignKey(
                        name: "FK_ProjectApplicationRespondentAnswers_ProjectApplicationRespondents_Id",
                        column: x => x.Id,
                        principalTable: "ProjectApplicationRespondents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectApplicationRespondentAnswers_ProjectApplications_ProjectApplicationId",
                        column: x => x.ProjectApplicationId,
                        principalTable: "ProjectApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectApplicationRespondentAnswers_ProjectApplicationId",
                table: "ProjectApplicationRespondentAnswers",
                column: "ProjectApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectApplications_ProjectApplicationRespondentId",
                table: "ProjectApplications",
                column: "ProjectApplicationRespondentId");

            migrationBuilder.CreateIndex(
                name: "IX_RegulatoryBodyAuditTrial_RegulatoryBodiesId",
                table: "RegulatoryBodyAuditTrial",
                column: "RegulatoryBodiesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectApplicationRespondentAnswers");

            migrationBuilder.DropTable(
                name: "RegulatoryBodyAuditTrial");

            migrationBuilder.DropTable(
                name: "RegulatoryBodyUsers");

            migrationBuilder.DropTable(
                name: "ProjectApplications");

            migrationBuilder.DropTable(
                name: "RegulatoryBodies");

            migrationBuilder.DropTable(
                name: "ProjectApplicationRespondents");

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
