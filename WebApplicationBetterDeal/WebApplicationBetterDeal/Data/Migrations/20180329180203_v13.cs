using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplicationBetterDeal.Data.Migrations
{
    public partial class v13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DontKnow",
                table: "Publication");

            migrationBuilder.DropColumn(
                name: "No",
                table: "Publication");

            migrationBuilder.DropColumn(
                name: "Yes",
                table: "Publication");

            migrationBuilder.CreateTable(
                name: "Response",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    DontKnow = table.Column<int>(nullable: false),
                    No = table.Column<int>(nullable: false),
                    PublicationId = table.Column<string>(nullable: true),
                    PublicationId1 = table.Column<int>(nullable: true),
                    Yes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Response", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Response_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Response_Publication_PublicationId1",
                        column: x => x.PublicationId1,
                        principalTable: "Publication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Response_ApplicationUserId",
                table: "Response",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Response_PublicationId1",
                table: "Response",
                column: "PublicationId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Response");

            migrationBuilder.AddColumn<int>(
                name: "DontKnow",
                table: "Publication",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "No",
                table: "Publication",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Yes",
                table: "Publication",
                nullable: false,
                defaultValue: 0);
        }
    }
}
