using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRespondentAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SelectedOptions",
                table: "RespondentAnswers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "OptionType",
                table: "RespondentAnswers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OptionType",
                table: "RespondentAnswers");

            migrationBuilder.AlterColumn<string>(
                name: "SelectedOptions",
                table: "RespondentAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
