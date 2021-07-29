using Microsoft.EntityFrameworkCore.Migrations;

namespace UTCAPPCMS.DAL.Migrations
{
    public partial class appsett : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "aboutUs",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "howItWorks",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "instgram",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "shareMessage",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "termCondition",
                table: "AppSettings");

            migrationBuilder.AddColumn<string>(
                name: "aboutUsAr",
                table: "AppSettings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "aboutUsEn",
                table: "AppSettings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "howItWorksAr",
                table: "AppSettings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "howItWorksEn",
                table: "AppSettings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "instagram",
                table: "AppSettings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "shareMessageAr",
                table: "AppSettings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "shareMessageEn",
                table: "AppSettings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "termConditionAr",
                table: "AppSettings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "termConditionEn",
                table: "AppSettings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "aboutUsAr",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "aboutUsEn",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "howItWorksAr",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "howItWorksEn",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "instagram",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "shareMessageAr",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "shareMessageEn",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "termConditionAr",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "termConditionEn",
                table: "AppSettings");

            migrationBuilder.AddColumn<string>(
                name: "aboutUs",
                table: "AppSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "howItWorks",
                table: "AppSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "instgram",
                table: "AppSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "shareMessage",
                table: "AppSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "termCondition",
                table: "AppSettings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
