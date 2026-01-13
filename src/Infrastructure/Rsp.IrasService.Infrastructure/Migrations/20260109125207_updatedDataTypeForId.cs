using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedDataTypeForId : Migration
    {
        /// <inheritdoc />

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1) Drop PK first
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectClosures",
                table: "ProjectClosures");

            // 2) Alter the column
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "ProjectClosures",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)"); // adjust oldType to match actual

            // 3) Recreate PK
            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectClosures",
                table: "ProjectClosures",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectClosures",
                table: "ProjectClosures");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "ProjectClosures",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectClosures",
                table: "ProjectClosures",
                column: "Id");
        }
    }
}