using Microsoft.EntityFrameworkCore.Migrations;

namespace UTCAPPCMS.DAL.Migrations
{
    public partial class UpdateParking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsMullti",
                table: "SubscriptionDurations",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AllTime",
                table: "SubscriptionDurations",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AllDays",
                table: "SubscriptionDurations",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupPrivilageId",
                table: "Parking",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<bool>(
                name: "IsMullti",
                table: "SubscriptionDurations",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "AllTime",
                table: "SubscriptionDurations",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "AllDays",
                table: "SubscriptionDurations",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
