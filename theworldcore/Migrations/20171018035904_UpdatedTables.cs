using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace theworldcore.Migrations
{
    public partial class UpdatedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Longtitude",
                table: "Stops");

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Stops",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Stops");

            migrationBuilder.AddColumn<double>(
                name: "Longtitude",
                table: "Stops",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
