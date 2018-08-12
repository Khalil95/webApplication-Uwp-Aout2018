using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplicationBetterDeal.Data.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonId1",
                table: "Publication",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publication_PersonId1",
                table: "Publication",
                column: "PersonId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Publication_AspNetUsers_PersonId1",
                table: "Publication",
                column: "PersonId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publication_AspNetUsers_PersonId1",
                table: "Publication");

            migrationBuilder.DropIndex(
                name: "IX_Publication_PersonId1",
                table: "Publication");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                table: "Publication");
        }
    }
}
