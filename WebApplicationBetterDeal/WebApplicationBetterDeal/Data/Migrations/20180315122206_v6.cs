using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplicationBetterDeal.Data.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressShop",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameShop",
                table: "Person",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressShop",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "NameShop",
                table: "Person");
        }
    }
}
