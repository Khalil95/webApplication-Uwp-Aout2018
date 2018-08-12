using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplicationBetterDeal.Data.Migrations
{
    public partial class v14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DontKnow",
                table: "Response");

            migrationBuilder.DropColumn(
                name: "No",
                table: "Response");

            migrationBuilder.DropColumn(
                name: "Yes",
                table: "Response");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DontKnow",
                table: "Response",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "No",
                table: "Response",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Yes",
                table: "Response",
                nullable: false,
                defaultValue: 0);
        }
    }
}
