using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Team12EUP.Migrations
{
    public partial class vs3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mark",
                table: "tests",
                newName: "TotalQuestion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalQuestion",
                table: "tests",
                newName: "Mark");
        }
    }
}
