using Microsoft.EntityFrameworkCore.Migrations;

namespace UTCAPPCMS.DAL.Migrations
{
    public partial class UpdateParkingHasManyPriv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParkingId",
                table: "GroupPrivilage",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupPrivilage_ParkingId",
                table: "GroupPrivilage",
                column: "ParkingId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPrivilage_Parking_ParkingId",
                table: "GroupPrivilage",
                column: "ParkingId",
                principalTable: "Parking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPrivilage_Parking_ParkingId",
                table: "GroupPrivilage");

            migrationBuilder.DropIndex(
                name: "IX_GroupPrivilage_ParkingId",
                table: "GroupPrivilage");

            migrationBuilder.DropColumn(
                name: "ParkingId",
                table: "GroupPrivilage");
        }
    }
}
