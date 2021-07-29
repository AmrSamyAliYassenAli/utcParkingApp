using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UTCAPPCMS.DAL.Migrations
{
    public partial class ImagePassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Customer");

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordHash",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordSalt",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "profileImage",
                table: "Customer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "passwordSalt",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "profileImage",
                table: "Customer");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
