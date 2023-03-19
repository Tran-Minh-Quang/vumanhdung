using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Team12EUP.Migrations
{
    public partial class vs9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HistoryTest",
                table: "tests");

            migrationBuilder.AddColumn<Guid>(
                name: "TestId",
                table: "historyTests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestId",
                table: "historyTests");

            migrationBuilder.AddColumn<Guid>(
                name: "HistoryTest",
                table: "tests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
