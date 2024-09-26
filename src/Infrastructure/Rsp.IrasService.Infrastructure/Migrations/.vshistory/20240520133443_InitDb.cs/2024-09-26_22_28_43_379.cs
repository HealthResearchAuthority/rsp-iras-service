using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "ResearchApplications",
               columns: table => new
               {
                   ApplicationId = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   IsActive = table.Column<bool>(type: "bit", nullable: false),
                   Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                   UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                   CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
               },
               constraints: table => table.PrimaryKey("PK_ResearchApplications", x => x.ApplicationId));

            migrationBuilder.CreateTable(
                name: "Respondents",
                columns: table => new
                {
                    RespondentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table => table.PrimaryKey("PK_Respondents", x => x.RespondentId));

            migrationBuilder.CreateTable(
               name: "RespondentAnswers",
               columns: table => new
               {
                   RespondentId = table.Column<int>(type: "int", nullable: false),
                   QuestionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                   ApplicationId = table.Column<int>(type: "int", nullable: false),
                   Response = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   OptionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                   EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                   Version = table.Column<string>(type: "nvarchar(max)", nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_RespondentAnswers", x => new { x.RespondentId, x.QuestionId, x.ApplicationId });
                   table.ForeignKey(
                       name: "FK_RespondentAnswers_Respondents_RespondentId",
                       column: x => x.RespondentId,
                       principalTable: "Respondents",
                       principalColumn: "RespondentId",
                       onDelete: ReferentialAction.Cascade);
               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IrasApplications");
        }
    }
}