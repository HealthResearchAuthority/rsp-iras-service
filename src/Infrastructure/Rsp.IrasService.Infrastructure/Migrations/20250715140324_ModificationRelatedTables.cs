﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rsp.IrasService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModificationRelatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VersionId",
                table: "ProjectRecordAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VersionId",
                table: "ProjectModificationAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModificationParticipatingOrganisations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectModificationChangeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganisationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectPersonnelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectRecordId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificationParticipatingOrganisations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModificationParticipatingOrganisations_ProjectModificationChanges_ProjectModificationChangeId",
                        column: x => x.ProjectModificationChangeId,
                        principalTable: "ProjectModificationChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModificationParticipatingOrganisations_ProjectPersonnels_ProjectPersonnelId",
                        column: x => x.ProjectPersonnelId,
                        principalTable: "ProjectPersonnels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModificationParticipatingOrganisations_ProjectRecords_ProjectRecordId",
                        column: x => x.ProjectRecordId,
                        principalTable: "ProjectRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ModificationDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectModificationChangeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectPersonnelId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectRecordId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DocumentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: true),
                    SponsorDocumentVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasPreviousVersion = table.Column<bool>(type: "bit", nullable: true),
                    SponsorDocumentDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificationDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModificationDocuments_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModificationDocuments_ProjectModificationChanges_ProjectModificationChangeId",
                        column: x => x.ProjectModificationChangeId,
                        principalTable: "ProjectModificationChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModificationDocuments_ProjectPersonnels_ProjectPersonnelId",
                        column: x => x.ProjectPersonnelId,
                        principalTable: "ProjectPersonnels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModificationDocuments_ProjectRecords_ProjectRecordId",
                        column: x => x.ProjectRecordId,
                        principalTable: "ProjectRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ModificationParticipatingOrganisationAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModificationParticipatingOrganisationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VersionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectedOptions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificationParticipatingOrganisationAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModificationParticipatingOrganisationAnswers_ModificationParticipatingOrganisations_ModificationParticipatingOrganisationId",
                        column: x => x.ModificationParticipatingOrganisationId,
                        principalTable: "ModificationParticipatingOrganisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "0bdbe9f6-ad47-4047-96e8-6252183e62c9", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "0c7c0fc1-0576-434f-b669-f5cb3bc97abd", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "13a9fb24-7fd8-4a99-93ae-a850f2244255", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "15a18460-5fcd-42f3-adbf-3f6517831dd5", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "1cabb7b7-d513-4e42-ac1f-63f692656367", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "2ec5b24e-0d9a-4745-b0fb-1e593d3123c2", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "38b8f121-25c7-4b76-941b-1b5143e83db4", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "48f4cf52-45eb-4628-8d81-95a9a9ad961b", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "4c565f6b-d87e-4bc3-b404-e70f15f07256", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "581806f0-efb0-4630-8894-aa45b08dc233", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "5b74e181-adf9-49ef-9b08-e9c5fe5bb13e", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "5ea1d104-368b-4a72-be78-aefcc1325d87", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "5ec03122-6bcd-4acd-b1dd-4e517c327de8", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "78442e04-9ecd-46a7-9a60-fd45cc065d60", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "7b7deced-a6fd-4c56-91d5-3bec8d86513d", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "7bb298b0-5f66-4d96-a94d-1c6a1eec1fb6", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "7f1743ff-2cde-41cb-bbdc-1ef393dbc746", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "8621f884-27e7-4fa6-915b-dda058f899ab", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "8ba9958f-79d0-4ad3-9a06-ff8ebceca508", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "b591f0c5-860d-42af-9ec9-6ac2bd7757b9", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "b720dc5c-f5ed-4b74-8847-3008e7fb132e", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "b8f9aade-b6ce-4c89-a8da-d645c65504b3", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "bc7a58bd-c84b-4319-8953-3271e6abdd8e", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "bec28d3f-a7ee-48b5-bf70-fb7056ca017a", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "cbad81e0-f16b-4751-9edb-f35bdb35f931", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "ce252b0d-2428-40a9-9033-d60361f2b741", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "d1eb154b-9921-477c-8f5f-2592ea44e245", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "dfcbe913-dcc0-497a-859d-9c4309c22499", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "e1ebe9dc-c554-4214-baf8-ef5410ac5477", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "e71883ef-f548-43ae-94bd-c83b11e6457b", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "f047d051-1498-446f-bfb5-462fb5522b68", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "ff7fd51d-ee44-4269-99ff-a965f87d1269", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "ffb6f01f-9614-4ced-84fa-ef8c0d8696e3", "IQA0002" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "0bdbe9f6-ad47-4047-96e8-6252183e62c9", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "0c7c0fc1-0576-434f-b669-f5cb3bc97abd", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "13a9fb24-7fd8-4a99-93ae-a850f2244255", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "15a18460-5fcd-42f3-adbf-3f6517831dd5", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "1cabb7b7-d513-4e42-ac1f-63f692656367", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "2ec5b24e-0d9a-4745-b0fb-1e593d3123c2", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "38b8f121-25c7-4b76-941b-1b5143e83db4", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "48f4cf52-45eb-4628-8d81-95a9a9ad961b", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "4c565f6b-d87e-4bc3-b404-e70f15f07256", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "581806f0-efb0-4630-8894-aa45b08dc233", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "5b74e181-adf9-49ef-9b08-e9c5fe5bb13e", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "5ea1d104-368b-4a72-be78-aefcc1325d87", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "5ec03122-6bcd-4acd-b1dd-4e517c327de8", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "78442e04-9ecd-46a7-9a60-fd45cc065d60", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "7b7deced-a6fd-4c56-91d5-3bec8d86513d", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "7bb298b0-5f66-4d96-a94d-1c6a1eec1fb6", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "7f1743ff-2cde-41cb-bbdc-1ef393dbc746", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "8621f884-27e7-4fa6-915b-dda058f899ab", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "8ba9958f-79d0-4ad3-9a06-ff8ebceca508", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "b591f0c5-860d-42af-9ec9-6ac2bd7757b9", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "b720dc5c-f5ed-4b74-8847-3008e7fb132e", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "b8f9aade-b6ce-4c89-a8da-d645c65504b3", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "bc7a58bd-c84b-4319-8953-3271e6abdd8e", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "bec28d3f-a7ee-48b5-bf70-fb7056ca017a", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "cbad81e0-f16b-4751-9edb-f35bdb35f931", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "ce252b0d-2428-40a9-9033-d60361f2b741", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "d1eb154b-9921-477c-8f5f-2592ea44e245", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "dfcbe913-dcc0-497a-859d-9c4309c22499", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "e1ebe9dc-c554-4214-baf8-ef5410ac5477", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "e71883ef-f548-43ae-94bd-c83b11e6457b", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "f047d051-1498-446f-bfb5-462fb5522b68", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "ff7fd51d-ee44-4269-99ff-a965f87d1269", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "ffb6f01f-9614-4ced-84fa-ef8c0d8696e3", "IQA0005" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "0bdbe9f6-ad47-4047-96e8-6252183e62c9", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "0c7c0fc1-0576-434f-b669-f5cb3bc97abd", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "13a9fb24-7fd8-4a99-93ae-a850f2244255", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "15a18460-5fcd-42f3-adbf-3f6517831dd5", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "1cabb7b7-d513-4e42-ac1f-63f692656367", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "2ec5b24e-0d9a-4745-b0fb-1e593d3123c2", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "38b8f121-25c7-4b76-941b-1b5143e83db4", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "48f4cf52-45eb-4628-8d81-95a9a9ad961b", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "4c565f6b-d87e-4bc3-b404-e70f15f07256", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "581806f0-efb0-4630-8894-aa45b08dc233", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "5b74e181-adf9-49ef-9b08-e9c5fe5bb13e", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "5ea1d104-368b-4a72-be78-aefcc1325d87", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "5ec03122-6bcd-4acd-b1dd-4e517c327de8", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "78442e04-9ecd-46a7-9a60-fd45cc065d60", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "7b7deced-a6fd-4c56-91d5-3bec8d86513d", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "7bb298b0-5f66-4d96-a94d-1c6a1eec1fb6", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "7f1743ff-2cde-41cb-bbdc-1ef393dbc746", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "8621f884-27e7-4fa6-915b-dda058f899ab", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "8ba9958f-79d0-4ad3-9a06-ff8ebceca508", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "b591f0c5-860d-42af-9ec9-6ac2bd7757b9", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "b720dc5c-f5ed-4b74-8847-3008e7fb132e", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "b8f9aade-b6ce-4c89-a8da-d645c65504b3", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "bc7a58bd-c84b-4319-8953-3271e6abdd8e", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "bec28d3f-a7ee-48b5-bf70-fb7056ca017a", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "cbad81e0-f16b-4751-9edb-f35bdb35f931", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "ce252b0d-2428-40a9-9033-d60361f2b741", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "d1eb154b-9921-477c-8f5f-2592ea44e245", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "dfcbe913-dcc0-497a-859d-9c4309c22499", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "e1ebe9dc-c554-4214-baf8-ef5410ac5477", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "e71883ef-f548-43ae-94bd-c83b11e6457b", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "f047d051-1498-446f-bfb5-462fb5522b68", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "ff7fd51d-ee44-4269-99ff-a965f87d1269", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "ffb6f01f-9614-4ced-84fa-ef8c0d8696e3", "IQA0311" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "0bdbe9f6-ad47-4047-96e8-6252183e62c9", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "0c7c0fc1-0576-434f-b669-f5cb3bc97abd", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "13a9fb24-7fd8-4a99-93ae-a850f2244255", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "15a18460-5fcd-42f3-adbf-3f6517831dd5", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "1cabb7b7-d513-4e42-ac1f-63f692656367", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "2ec5b24e-0d9a-4745-b0fb-1e593d3123c2", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "38b8f121-25c7-4b76-941b-1b5143e83db4", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "48f4cf52-45eb-4628-8d81-95a9a9ad961b", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "4c565f6b-d87e-4bc3-b404-e70f15f07256", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "581806f0-efb0-4630-8894-aa45b08dc233", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "5b74e181-adf9-49ef-9b08-e9c5fe5bb13e", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "5ea1d104-368b-4a72-be78-aefcc1325d87", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "5ec03122-6bcd-4acd-b1dd-4e517c327de8", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "78442e04-9ecd-46a7-9a60-fd45cc065d60", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "7b7deced-a6fd-4c56-91d5-3bec8d86513d", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "7bb298b0-5f66-4d96-a94d-1c6a1eec1fb6", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "7f1743ff-2cde-41cb-bbdc-1ef393dbc746", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "8621f884-27e7-4fa6-915b-dda058f899ab", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "8ba9958f-79d0-4ad3-9a06-ff8ebceca508", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "b591f0c5-860d-42af-9ec9-6ac2bd7757b9", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "b720dc5c-f5ed-4b74-8847-3008e7fb132e", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "b8f9aade-b6ce-4c89-a8da-d645c65504b3", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "bc7a58bd-c84b-4319-8953-3271e6abdd8e", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "bec28d3f-a7ee-48b5-bf70-fb7056ca017a", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "cbad81e0-f16b-4751-9edb-f35bdb35f931", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "ce252b0d-2428-40a9-9033-d60361f2b741", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "d1eb154b-9921-477c-8f5f-2592ea44e245", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "dfcbe913-dcc0-497a-859d-9c4309c22499", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "e1ebe9dc-c554-4214-baf8-ef5410ac5477", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "e71883ef-f548-43ae-94bd-c83b11e6457b", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "f047d051-1498-446f-bfb5-462fb5522b68", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "ff7fd51d-ee44-4269-99ff-a965f87d1269", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.UpdateData(
                table: "ProjectRecordAnswers",
                keyColumns: new[] { "ProjectPersonnelId", "ProjectRecordId", "QuestionId" },
                keyValues: new object[] { "369e5231-45ec-4f20-bbc2-4940959be518", "ffb6f01f-9614-4ced-84fa-ef8c0d8696e3", "IQA0312" },
                column: "VersionId",
                value: "1.0.0");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationDocuments_DocumentTypeId",
                table: "ModificationDocuments",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationDocuments_ProjectModificationChangeId",
                table: "ModificationDocuments",
                column: "ProjectModificationChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationDocuments_ProjectPersonnelId",
                table: "ModificationDocuments",
                column: "ProjectPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationDocuments_ProjectRecordId",
                table: "ModificationDocuments",
                column: "ProjectRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationParticipatingOrganisationAnswers_ModificationParticipatingOrganisationId",
                table: "ModificationParticipatingOrganisationAnswers",
                column: "ModificationParticipatingOrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationParticipatingOrganisations_ProjectModificationChangeId",
                table: "ModificationParticipatingOrganisations",
                column: "ProjectModificationChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationParticipatingOrganisations_ProjectPersonnelId",
                table: "ModificationParticipatingOrganisations",
                column: "ProjectPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationParticipatingOrganisations_ProjectRecordId",
                table: "ModificationParticipatingOrganisations",
                column: "ProjectRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModificationDocuments");

            migrationBuilder.DropTable(
                name: "ModificationParticipatingOrganisationAnswers");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "ModificationParticipatingOrganisations");

            migrationBuilder.DropColumn(
                name: "VersionId",
                table: "ProjectRecordAnswers");

            migrationBuilder.DropColumn(
                name: "VersionId",
                table: "ProjectModificationAnswers");
        }
    }
}
