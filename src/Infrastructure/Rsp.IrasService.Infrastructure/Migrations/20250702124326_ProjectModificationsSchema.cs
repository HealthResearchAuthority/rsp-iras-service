using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProjectModificationsSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectModifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectRecordId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModificationNumber = table.Column<int>(type: "int", nullable: false),
                    ModificationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectModifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectModifications_ProjectRecords_ProjectRecordId",
                        column: x => x.ProjectRecordId,
                        principalTable: "ProjectRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectModificationChanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectModificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AreaOfChange = table.Column<int>(type: "int", nullable: false),
                    SpecificAreaOfChange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectModificationChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectModificationChanges_ProjectModifications_ProjectModificationId",
                        column: x => x.ProjectModificationId,
                        principalTable: "ProjectModifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectModificationAnswers",
                columns: table => new
                {
                    ProjectModificationChangeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectPersonnelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectRecordId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectedOptions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectModificationAnswers", x => new { x.ProjectModificationChangeId, x.QuestionId, x.ProjectPersonnelId });
                    table.ForeignKey(
                        name: "FK_ProjectModificationAnswers_ProjectModificationChanges_ProjectModificationChangeId",
                        column: x => x.ProjectModificationChangeId,
                        principalTable: "ProjectModificationChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectModificationAnswers_ProjectPersonnels_ProjectPersonnelId",
                        column: x => x.ProjectPersonnelId,
                        principalTable: "ProjectPersonnels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectModificationAnswers_ProjectRecords_ProjectRecordId",
                        column: x => x.ProjectRecordId,
                        principalTable: "ProjectRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationAnswers_ProjectPersonnelId",
                table: "ProjectModificationAnswers",
                column: "ProjectPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationAnswers_ProjectRecordId",
                table: "ProjectModificationAnswers",
                column: "ProjectRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationChanges_ProjectModificationId",
                table: "ProjectModificationChanges",
                column: "ProjectModificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModifications_ProjectRecordId",
                table: "ProjectModifications",
                column: "ProjectRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectModificationAnswers");

            migrationBuilder.DropTable(
                name: "ProjectModificationChanges");

            migrationBuilder.DropTable(
                name: "ProjectModifications");
        }
    }
}