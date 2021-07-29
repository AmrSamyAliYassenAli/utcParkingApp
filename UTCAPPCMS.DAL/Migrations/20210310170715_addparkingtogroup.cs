using Microsoft.EntityFrameworkCore.Migrations;

namespace UTCAPPCMS.DAL.Migrations
{
    public partial class addparkingtogroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParkingId",
                table: "UserGroup",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_ParkingId",
                table: "UserGroup",
                column: "ParkingId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroup_Parking_ParkingId",
                table: "UserGroup",
                column: "ParkingId",
                principalTable: "Parking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_Parking_ParkingId",
                table: "UserGroup");

            migrationBuilder.DropIndex(
                name: "IX_UserGroup_ParkingId",
                table: "UserGroup");

            migrationBuilder.DropColumn(
                name: "ParkingId",
                table: "UserGroup");
        }
    }
}
