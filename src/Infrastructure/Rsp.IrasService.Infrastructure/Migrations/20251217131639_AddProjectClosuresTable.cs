using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectClosuresTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectClosures",
                columns: table => new
                {
                    TransactionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectRecordId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClosureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentToSponsorDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateActioned = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShortProjectTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IrasId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectClosures", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_ProjectClosures_ProjectRecords_ProjectRecordId",
                        column: x => x.ProjectRecordId,
                        principalTable: "ProjectRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectClosures_ProjectRecordId",
                table: "ProjectClosures",
                column: "ProjectRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectClosures");
        }
    }
}
