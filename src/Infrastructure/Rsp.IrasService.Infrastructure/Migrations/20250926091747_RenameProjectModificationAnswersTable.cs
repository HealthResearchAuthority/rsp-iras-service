using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameProjectModificationAnswersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // rename table without loosing data
            migrationBuilder.DropPrimaryKey("PK_ProjectModificationAnswers", "ProjectModificationAnswers");

            migrationBuilder.DropForeignKey("FK_ProjectModificationAnswers_ProjectModificationChanges_ProjectModificationChangeId", "ProjectModificationAnswers");
            migrationBuilder.DropForeignKey("FK_ProjectModificationAnswers_ProjectPersonnels_ProjectPersonnelId", "ProjectModificationAnswers");
            migrationBuilder.DropForeignKey("FK_ProjectModificationAnswers_ProjectRecords_ProjectRecordId", "ProjectModificationAnswers");

            migrationBuilder.DropIndex("IX_ProjectModificationAnswers_ProjectPersonnelId", "ProjectModificationAnswers");
            migrationBuilder.DropIndex("IX_ProjectModificationAnswers_ProjectRecordId", "ProjectModificationAnswers");

            migrationBuilder.RenameTable(name: "ProjectModificationAnswers", newName: "ProjectModificationChangeAnswers");

            migrationBuilder.AddPrimaryKey("PK_ProjectModificationChangeAnswers", "ProjectModificationChangeAnswers", ["ProjectModificationChangeId", "QuestionId", "ProjectPersonnelId"]);
            migrationBuilder.AddForeignKey
            (
                name: "FK_ProjectModificationChangeAnswers_ProjectModificationChanges_ProjectModificationChangeId",
                table: "ProjectModificationChangeAnswers",
                column: "ProjectModificationChangeId",
                principalTable: "ProjectModificationChanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey
            (
                name: "FK_ProjectModificationChangeAnswers_ProjectPersonnels_ProjectPersonnelId",
                table: "ProjectModificationChangeAnswers",
                column: "ProjectPersonnelId",
                principalTable: "ProjectPersonnels",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey
            (
                name: "FK_ProjectModificationChangeAnswers_ProjectRecords_ProjectRecordId",
                table: "ProjectModificationChangeAnswers",
                column: "ProjectRecordId",
                principalTable: "ProjectRecords",
                principalColumn: "Id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationChangeAnswers_ProjectPersonnelId",
                table: "ProjectModificationChangeAnswers",
                column: "ProjectPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationChangeAnswers_ProjectRecordId",
                table: "ProjectModificationChangeAnswers",
                column: "ProjectRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // rename table without loosing data
            migrationBuilder.DropPrimaryKey("PK_ProjectModificationChangeAnswers", "ProjectModificationChangeAnswers");

            migrationBuilder.DropForeignKey("FK_ProjectModificationChangeAnswers_ProjectModificationChanges_ProjectModificationChangeId", "ProjectModificationChangeAnswers");
            migrationBuilder.DropForeignKey("FK_ProjectModificationChangeAnswers_ProjectPersonnels_ProjectPersonnelId", "ProjectModificationChangeAnswers");
            migrationBuilder.DropForeignKey("FK_ProjectModificationChangeAnswers_ProjectRecords_ProjectRecordId", "ProjectModificationChangeAnswers");

            migrationBuilder.DropIndex("IX_ProjectModificationChangeAnswers_ProjectPersonnelId", "ProjectModificationChangeAnswers");
            migrationBuilder.DropIndex("IX_ProjectModificationChangeAnswers_ProjectRecordId", "ProjectModificationChangeAnswers");

            migrationBuilder.RenameTable(name: "ProjectModificationChangeAnswers", newName: "ProjectModificationAnswers");

            migrationBuilder.AddPrimaryKey("PK_ProjectModificationAnswers", "ProjectModificationAnswers", ["ProjectModificationChangeId", "QuestionId", "ProjectPersonnelId"]);
            migrationBuilder.AddForeignKey
            (
                name: "FK_ProjectModificationAnswers_ProjectModificationChanges_ProjectModificationChangeId",
                table: "ProjectModificationAnswers",
                column: "ProjectModificationChangeId",
                principalTable: "ProjectModificationChanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey
            (
                name: "FK_ProjectModificationAnswers_ProjectPersonnels_ProjectPersonnelId",
                table: "ProjectModificationAnswers",
                column: "ProjectPersonnelId",
                principalTable: "ProjectPersonnels",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey
            (
                name: "FK_ProjectModificationAnswers_ProjectRecords_ProjectRecordId",
                table: "ProjectModificationAnswers",
                column: "ProjectRecordId",
                principalTable: "ProjectRecords",
                principalColumn: "Id"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationAnswers_ProjectPersonnelId",
                table: "ProjectModificationAnswers",
                column: "ProjectPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectModificationAnswers_ProjectRecordId",
                table: "ProjectModificationAnswers",
                column: "ProjectRecordId");
        }
    }
}