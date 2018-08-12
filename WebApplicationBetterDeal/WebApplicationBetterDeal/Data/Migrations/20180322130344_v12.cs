using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplicationBetterDeal.Data.Migrations
{
    public partial class v12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publication_AspNetUsers_ApplicationUserId1",
                table: "Publication");

            migrationBuilder.DropForeignKey(
                name: "FK_Publication_AspNetUsers_ApplicationUserIdId",
                table: "Publication");

            migrationBuilder.DropIndex(
                name: "IX_Publication_ApplicationUserId1",
                table: "Publication");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Publication");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserIdId",
                table: "Publication",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Publication_ApplicationUserIdId",
                table: "Publication",
                newName: "IX_Publication_ApplicationUserId");

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

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Publication",
                newName: "ApplicationUserIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Publication_ApplicationUserId",
                table: "Publication",
                newName: "IX_Publication_ApplicationUserIdId");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Publication",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publication_ApplicationUserId1",
                table: "Publication",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Publication_AspNetUsers_ApplicationUserId1",
                table: "Publication",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Publication_AspNetUsers_ApplicationUserIdId",
                table: "Publication",
                column: "ApplicationUserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
