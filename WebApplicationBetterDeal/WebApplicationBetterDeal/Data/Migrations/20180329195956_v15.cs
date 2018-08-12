using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplicationBetterDeal.Data.Migrations
{
    public partial class v15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Response_Publication_PublicationId1",
                table: "Response");

            migrationBuilder.DropIndex(
                name: "IX_Response_PublicationId1",
                table: "Response");

            migrationBuilder.DropColumn(
                name: "PublicationId1",
                table: "Response");

            migrationBuilder.AlterColumn<int>(
                name: "PublicationId",
                table: "Response",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "response",
                table: "Response",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Response_PublicationId",
                table: "Response",
                column: "PublicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Response_Publication_PublicationId",
                table: "Response",
                column: "PublicationId",
                principalTable: "Publication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Response_Publication_PublicationId",
                table: "Response");

            migrationBuilder.DropIndex(
                name: "IX_Response_PublicationId",
                table: "Response");

            migrationBuilder.DropColumn(
                name: "response",
                table: "Response");

            migrationBuilder.AlterColumn<string>(
                name: "PublicationId",
                table: "Response",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PublicationId1",
                table: "Response",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Response_PublicationId1",
                table: "Response",
                column: "PublicationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Response_Publication_PublicationId1",
                table: "Response",
                column: "PublicationId1",
                principalTable: "Publication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
