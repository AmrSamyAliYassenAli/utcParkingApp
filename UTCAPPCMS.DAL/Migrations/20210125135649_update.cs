using Microsoft.EntityFrameworkCore.Migrations;

namespace UTCAPPCMS.DAL.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Table_TransactionDetail_ParkingLocations_ParkingLocationIdId",
                table: "Table_TransactionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_TransactionDetail_PaymentType_PaymentTypeId",
                table: "Table_TransactionDetail");

            migrationBuilder.DropIndex(
                name: "IX_Table_TransactionDetail_ParkingLocationIdId",
                table: "Table_TransactionDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentType",
                table: "PaymentType");

            migrationBuilder.DropColumn(
                name: "ParkingLocationIdId",
                table: "Table_TransactionDetail");

            migrationBuilder.RenameTable(
                name: "PaymentType",
                newName: "PaymentTypes");

            migrationBuilder.AddColumn<int>(
                name: "ParkingLocationId",
                table: "Table_TransactionDetail",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentTypes",
                table: "PaymentTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Table_TransactionDetail_PaymentTypes_PaymentTypeId",
                table: "Table_TransactionDetail",
                column: "PaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Table_TransactionDetail_PaymentTypes_PaymentTypeId",
                table: "Table_TransactionDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentTypes",
                table: "PaymentTypes");

            migrationBuilder.DropColumn(
                name: "ParkingLocationId",
                table: "Table_TransactionDetail");

            migrationBuilder.RenameTable(
                name: "PaymentTypes",
                newName: "PaymentType");

            migrationBuilder.AddColumn<int>(
                name: "ParkingLocationIdId",
                table: "Table_TransactionDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentType",
                table: "PaymentType",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Table_TransactionDetail_ParkingLocationIdId",
                table: "Table_TransactionDetail",
                column: "ParkingLocationIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Table_TransactionDetail_ParkingLocations_ParkingLocationIdId",
                table: "Table_TransactionDetail",
                column: "ParkingLocationIdId",
                principalTable: "ParkingLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Table_TransactionDetail_PaymentType_PaymentTypeId",
                table: "Table_TransactionDetail",
                column: "PaymentTypeId",
                principalTable: "PaymentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
