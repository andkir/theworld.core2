using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace theworldcore.Migrations
{
    public partial class UpdatedTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Trips");
        }
    }
}
