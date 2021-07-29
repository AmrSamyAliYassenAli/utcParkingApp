using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UTCAPPCMS.DAL.Migrations
{
    public partial class addexpiredate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Table_CollectionTransaction_PaymentSource",
                table: "Table_CollectionTransaction");

            migrationBuilder.DropIndex(
                name: "IX_Table_CollectionTransaction_paymentsource_FK_ID",
                table: "Table_CollectionTransaction");

            migrationBuilder.DropColumn(
                name: "paymentsource_FK_ID",
                table: "Table_CollectionTransaction");

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentExpireDate",
                table: "Table_TransactionDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentSourceId",
                table: "Table_CollectionTransaction",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentTypeId",
                table: "Table_CollectionTransaction",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Table_CollectionTransaction_PaymentSourceId",
                table: "Table_CollectionTransaction",
                column: "PaymentSourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Table_CollectionTransaction_PaymentSource_PaymentSourceId",
                table: "Table_CollectionTransaction",
                column: "PaymentSourceId",
                principalTable: "PaymentSource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Table_CollectionTransaction_PaymentSource_PaymentSourceId",
                table: "Table_CollectionTransaction");

            migrationBuilder.DropIndex(
                name: "IX_Table_CollectionTransaction_PaymentSourceId",
                table: "Table_CollectionTransaction");

            migrationBuilder.DropColumn(
                name: "PaymentExpireDate",
                table: "Table_TransactionDetail");

            migrationBuilder.DropColumn(
                name: "PaymentSourceId",
                table: "Table_CollectionTransaction");

            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                table: "Table_CollectionTransaction");

            migrationBuilder.AddColumn<int>(
                name: "paymentsource_FK_ID",
                table: "Table_CollectionTransaction",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Table_CollectionTransaction_paymentsource_FK_ID",
                table: "Table_CollectionTransaction",
                column: "paymentsource_FK_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Table_CollectionTransaction_PaymentSource",
                table: "Table_CollectionTransaction",
                column: "paymentsource_FK_ID",
                principalTable: "PaymentSource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
