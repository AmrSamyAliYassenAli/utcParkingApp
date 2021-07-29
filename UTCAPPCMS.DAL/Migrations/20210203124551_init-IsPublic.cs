using Microsoft.EntityFrameworkCore.Migrations;

namespace UTCAPPCMS.DAL.Migrations
{
    public partial class initIsPublic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsPublic",
                table: "ParkingLocations",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsPublic",
                table: "ParkingLocations",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
