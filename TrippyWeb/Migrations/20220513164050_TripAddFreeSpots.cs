using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrippyWeb.Migrations
{
    public partial class TripAddFreeSpots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FreeSpots",
                table: "Trip",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreeSpots",
                table: "Trip");
        }
    }
}
