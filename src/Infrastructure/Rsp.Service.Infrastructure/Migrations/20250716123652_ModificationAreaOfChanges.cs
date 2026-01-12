using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModificationAreaOfChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModificationAreaOfChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificationAreaOfChanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModificationSpecificAreaOfChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JourneyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationAreaOfChangeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificationSpecificAreaOfChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModificationSpecificAreaOfChanges_ModificationAreaOfChanges_ModificationAreaOfChangeId",
                        column: x => x.ModificationAreaOfChangeId,
                        principalTable: "ModificationAreaOfChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModificationSpecificAreaOfChanges_ModificationAreaOfChangeId",
                table: "ModificationSpecificAreaOfChanges",
                column: "ModificationAreaOfChangeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModificationSpecificAreaOfChanges");

            migrationBuilder.DropTable(
                name: "ModificationAreaOfChanges");
        }
    }
}
