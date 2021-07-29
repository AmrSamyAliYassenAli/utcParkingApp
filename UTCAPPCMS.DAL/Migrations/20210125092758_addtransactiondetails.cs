using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UTCAPPCMS.DAL.Migrations
{
    public partial class addtransactiondetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Table_TransactionDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSynch",
                table: "Table_TransactionDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ParkingLocationIdId",
                table: "Table_TransactionDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingLocationsId",
                table: "Table_TransactionDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Table_TransactionDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentTypeId",
                table: "Table_TransactionDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SynchDate",
                table: "Table_TransactionDetail",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<int>(nullable: true),
                    ModifiedUserId = table.Column<int>(nullable: true),
                    IsEnable = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Table_TransactionDetail_ParkingLocationIdId",
                table: "Table_TransactionDetail",
                column: "ParkingLocationIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_TransactionDetail_ParkingLocationsId",
                table: "Table_TransactionDetail",
                column: "ParkingLocationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_TransactionDetail_PaymentTypeId",
                table: "Table_TransactionDetail",
                column: "PaymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Table_TransactionDetail_ParkingLocations_ParkingLocationIdId",
                table: "Table_TransactionDetail",
                column: "ParkingLocationIdId",
                principalTable: "ParkingLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Table_TransactionDetail_ParkingLocations_ParkingLocationsId",
                table: "Table_TransactionDetail",
                column: "ParkingLocationsId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Table_TransactionDetail_ParkingLocations_ParkingLocationIdId",
                table: "Table_TransactionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_TransactionDetail_ParkingLocations_ParkingLocationsId",
                table: "Table_TransactionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_TransactionDetail_PaymentType_PaymentTypeId",
                table: "Table_TransactionDetail");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropIndex(
                name: "IX_Table_TransactionDetail_ParkingLocationIdId",
                table: "Table_TransactionDetail");

            migrationBuilder.DropIndex(
                name: "IX_Table_TransactionDetail_ParkingLocationsId",
                table: "Table_TransactionDetail");

            migrationBuilder.DropIndex(
                name: "IX_Table_TransactionDetail_PaymentTypeId",
                table: "Table_TransactionDetail");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Table_TransactionDetail");

            migrationBuilder.DropColumn(
                name: "IsSynch",
                table: "Table_TransactionDetail");

            migrationBuilder.DropColumn(
                name: "ParkingLocationIdId",
                table: "Table_TransactionDetail");

            migrationBuilder.DropColumn(
                name: "ParkingLocationsId",
                table: "Table_TransactionDetail");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Table_TransactionDetail");

            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                table: "Table_TransactionDetail");

            migrationBuilder.DropColumn(
                name: "SynchDate",
                table: "Table_TransactionDetail");
        }
    }
}
