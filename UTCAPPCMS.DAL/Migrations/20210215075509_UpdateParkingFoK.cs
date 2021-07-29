using Microsoft.EntityFrameworkCore.Migrations;

namespace UTCAPPCMS.DAL.Migrations
{
    public partial class UpdateParkingFoK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parking_GroupPrivilage_GroupPrivilageId",
                table: "Parking");

            migrationBuilder.DropIndex(
                name: "IX_Parking_GroupPrivilageId",
                table: "Parking");

            migrationBuilder.DropColumn(
                name: "GroupPrivilageId",
                table: "Parking");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupPrivilageId",
                table: "Parking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Parking_GroupPrivilageId",
                table: "Parking",
                column: "GroupPrivilageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parking_GroupPrivilage_GroupPrivilageId",
                table: "Parking",
                column: "GroupPrivilageId",
                principalTable: "GroupPrivilage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
