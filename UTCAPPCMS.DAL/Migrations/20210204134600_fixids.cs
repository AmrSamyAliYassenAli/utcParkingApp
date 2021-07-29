using Microsoft.EntityFrameworkCore.Migrations;

namespace UTCAPPCMS.DAL.Migrations
{
    public partial class fixids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableTransactionDetail_ParkingLocations_ParkingLocationsId",
                table: "TableTransactionDetail");

            migrationBuilder.DropIndex(
                name: "IX_TableTransactionDetail_ParkingLocationsId",
                table: "TableTransactionDetail");

            migrationBuilder.DropColumn(
                name: "ParkingLocationsId",
                table: "TableTransactionDetail");

            migrationBuilder.CreateIndex(
                name: "IX_TableTransactionDetail_ParkingLocationId",
                table: "TableTransactionDetail",
                column: "ParkingLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TableTransactionDetail_ParkingLocations_ParkingLocationId",
                table: "TableTransactionDetail",
                column: "ParkingLocationId",
                principalTable: "ParkingLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableTransactionDetail_ParkingLocations_ParkingLocationId",
                table: "TableTransactionDetail");

            migrationBuilder.DropIndex(
                name: "IX_TableTransactionDetail_ParkingLocationId",
                table: "TableTransactionDetail");

            migrationBuilder.AddColumn<int>(
                name: "ParkingLocationsId",
                table: "TableTransactionDetail",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TableTransactionDetail_ParkingLocationsId",
                table: "TableTransactionDetail",
                column: "ParkingLocationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TableTransactionDetail_ParkingLocations_ParkingLocationsId",
                table: "TableTransactionDetail",
                column: "ParkingLocationsId",
                principalTable: "ParkingLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
