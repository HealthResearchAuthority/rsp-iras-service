using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewerToProjectModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReviewerId",
                table: "ProjectModifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("00d78745-cd89-45fe-809d-3a6098d5237c"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("08584199-64d9-4fd0-8fe3-45dc5e23f707"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("0c22f6ce-eca7-478f-a26c-0c97d9a2ab3f"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("13e4063b-fe2c-4817-bba8-8506970a47c7"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("13ea428f-fdff-4d62-85ab-43127e736bc9"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("1cb61841-8d38-42d1-93e6-46a165cf05de"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("2667cf04-255d-4215-9359-f8aa4e4535bb"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("29b8a58c-0049-498e-a496-2be5c96404aa"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("2ad787a7-1c9c-4e8b-bb06-0bba3f73d2c2"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("2bb56b29-ac7d-4006-bc7d-bff80a7e957d"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("3b2bc66a-db60-4b85-b34b-0bae8be749af"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("45b93cb8-8a7e-443f-a42b-b196a48bf931"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("52ff08d7-3002-49c8-8000-9f16a3effe7c"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("5650300e-1dd1-4378-9604-2d00dac42bfd"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("60d5a5d1-ba3d-472d-96ed-e167be034dc2"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("679003c7-4790-46cd-95fe-9deedb18e9de"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("6bcf16e4-2ea3-4ced-8f9a-641bc3f46690"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("748ac01f-2720-4522-a5e9-55360ef3dfa8"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("77334f17-f953-4614-91b8-f4d36cb2e60e"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("791f38a9-2070-40bd-bbd9-abed4fd445ab"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("7b0df10b-75ad-4c06-a1cf-e9b9576dab33"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("7c27abc3-fb01-4af9-bc9f-85604f53be14"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("7fa41b2a-f9c9-45f3-9cb3-cb7c21fcca94"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("81f6cf0a-591d-49f3-a5d9-62f3b480d43c"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("86f072c6-57ff-4c94-8fee-c36eedffae71"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("87995063-831b-47f5-8390-2d06cec257ef"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("88a538d2-d6fc-486c-a8b2-45a934bb364d"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("8bc41eb5-e329-45db-826e-d5ded59d6470"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("8cdebe8f-39ed-4010-bb27-dbd07c4bf3e9"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("97dd6304-01d6-4092-8887-1a1f69af826f"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("9c7f45a6-6d9f-4a96-9568-2818732c35d4"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("9ec39f6e-a356-4673-851a-e751d8238ba4"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("aa4f1ee0-74da-4f10-815d-758d8c583a6d"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("abc0a888-a6e1-426b-9404-7bd398c6f4c4"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("b217caab-f8d4-47ef-8a38-c261d9f2451a"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("b6997f28-53ef-4c3e-9e87-3005463d94f9"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("b715ff2a-17bb-4272-b720-5ace38d71ca0"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("b98ace51-2be7-457d-ab71-ee3c1b86c455"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("bbe8a6a9-7948-4df6-814a-415076675272"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("c2a77344-1ffb-4471-86c2-2b3030d04d89"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("c5dfca23-18fb-4d66-a538-6eb3f44a5e79"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("c7432ebe-1371-4c98-bc3e-d79962065277"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("c7d99371-7fda-4ed7-a0ab-732a6c3cd09b"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("cb15b965-4194-45b9-a9b3-79e56e367763"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("d2d47942-fcb9-4274-a8ee-44b266a63816"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("e05f73a0-4d19-43a0-aab5-2b0e9bc91cf0"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("e2bfe66a-c9d4-487a-a824-5536c1396d3a"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("e3a8def7-9df0-4afe-935d-d7585460c359"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("f1060235-a1ca-4633-b26d-e8dab2f7b92b"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("f43343de-2afe-4094-a5a6-ec7b01cd9ea9"),
                column: "ReviewerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProjectModifications",
                keyColumn: "Id",
                keyValue: new Guid("fd02c834-bc33-42ec-9067-9437aa5e305d"),
                column: "ReviewerId",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewerId",
                table: "ProjectModifications");
        }
    }
}