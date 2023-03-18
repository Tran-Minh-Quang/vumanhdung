﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Team12EUP.Migrations
{
    public partial class vs5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HistoryTest",
                table: "tests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HistoryTest",
                table: "tests");
        }
    }
}
