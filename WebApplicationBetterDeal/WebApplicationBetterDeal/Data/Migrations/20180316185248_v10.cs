using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplicationBetterDeal.Data.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Publication",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publication_ApplicationUserId",
                table: "Publication",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publication_AspNetUsers_ApplicationUserId",
                table: "Publication",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publication_AspNetUsers_ApplicationUserId",
                table: "Publication");

            migrationBuilder.DropIndex(
                name: "IX_Publication_ApplicationUserId",
                table: "Publication");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Publication");
        }
    }
}
