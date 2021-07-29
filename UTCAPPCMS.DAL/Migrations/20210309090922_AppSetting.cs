using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UTCAPPCMS.DAL.Migrations
{
    public partial class AppSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<int>(nullable: true),
                    ModifiedUserId = table.Column<int>(nullable: true),
                    IsEnable = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    shareLink = table.Column<string>(nullable: true),
                    linkedIn = table.Column<string>(nullable: true),
                    pinterest = table.Column<string>(nullable: true),
                    contactUsEmail = table.Column<string>(nullable: true),
                    contactUsPhone = table.Column<string>(nullable: true),
                    webSite = table.Column<string>(nullable: true),
                    facebook = table.Column<string>(nullable: true),
                    twitter = table.Column<string>(nullable: true),
                    instgram = table.Column<string>(nullable: true),
                    youtube = table.Column<string>(nullable: true),
                    aboutUs = table.Column<string>(nullable: true),
                    howItWorks = table.Column<string>(nullable: true),
                    termCondition = table.Column<string>(nullable: true),
                    shareMessage = table.Column<string>(nullable: true),
                    ParkingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSettings");
        }
    }
}
