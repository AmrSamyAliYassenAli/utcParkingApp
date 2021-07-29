using Microsoft.EntityFrameworkCore.Migrations;

namespace UTCAPPCMS.DAL.Migrations
{
    public partial class init_UTCAPPCMS_UPDATE3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPrivilage_UserGroup_GroupIdNavigationId",
                table: "GroupPrivilage");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPrivilage_FormPages_PageIdNavigationId",
                table: "GroupPrivilage");

            migrationBuilder.DropIndex(
                name: "IX_GroupPrivilage_GroupIdNavigationId",
                table: "GroupPrivilage");

            migrationBuilder.DropIndex(
                name: "IX_GroupPrivilage_PageIdNavigationId",
                table: "GroupPrivilage");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "GroupPrivilage");

            migrationBuilder.DropColumn(
                name: "GroupIdNavigationId",
                table: "GroupPrivilage");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "GroupPrivilage");

            migrationBuilder.DropColumn(
                name: "PageIdNavigationId",
                table: "GroupPrivilage");

            migrationBuilder.AddColumn<int>(
                name: "FormPagesId",
                table: "GroupPrivilage",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserGroupId",
                table: "GroupPrivilage",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupPrivilage_FormPagesId",
                table: "GroupPrivilage",
                column: "FormPagesId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPrivilage_UserGroupId",
                table: "GroupPrivilage",
                column: "UserGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPrivilage_FormPages_FormPagesId",
                table: "GroupPrivilage",
                column: "FormPagesId",
                principalTable: "FormPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPrivilage_UserGroup_UserGroupId",
                table: "GroupPrivilage",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPrivilage_FormPages_FormPagesId",
                table: "GroupPrivilage");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPrivilage_UserGroup_UserGroupId",
                table: "GroupPrivilage");

            migrationBuilder.DropIndex(
                name: "IX_GroupPrivilage_FormPagesId",
                table: "GroupPrivilage");

            migrationBuilder.DropIndex(
                name: "IX_GroupPrivilage_UserGroupId",
                table: "GroupPrivilage");

            migrationBuilder.DropColumn(
                name: "FormPagesId",
                table: "GroupPrivilage");

            migrationBuilder.DropColumn(
                name: "UserGroupId",
                table: "GroupPrivilage");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "GroupPrivilage",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupIdNavigationId",
                table: "GroupPrivilage",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageId",
                table: "GroupPrivilage",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageIdNavigationId",
                table: "GroupPrivilage",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupPrivilage_GroupIdNavigationId",
                table: "GroupPrivilage",
                column: "GroupIdNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPrivilage_PageIdNavigationId",
                table: "GroupPrivilage",
                column: "PageIdNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPrivilage_UserGroup_GroupIdNavigationId",
                table: "GroupPrivilage",
                column: "GroupIdNavigationId",
                principalTable: "UserGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPrivilage_FormPages_PageIdNavigationId",
                table: "GroupPrivilage",
                column: "PageIdNavigationId",
                principalTable: "FormPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
