using Microsoft.EntityFrameworkCore.Migrations;

namespace UTCAPPCMS.DAL.Migrations
{
    public partial class init_AreaFkTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingLocations_Area_Area_FK_ID",
                table: "ParkingLocations");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingLocations_Area",
                table: "ParkingLocations",
                column: "Area_FK_ID",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingLocations_Area",
                table: "ParkingLocations");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingLocations_Area_Area_FK_ID",
                table: "ParkingLocations",
                column: "Area_FK_ID",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
