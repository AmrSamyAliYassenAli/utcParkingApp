using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UTCAPPCMS.DAL.Migrations
{
    public partial class init_UTCAPPCMS_UPDATE2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminUsers_Parking",
                table: "AdminUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AdminUsers_UserType",
                table: "AdminUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Nationality_NationalityFkID",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Parking_ParkingFkID",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_SubscriptionDurations_SubscriptionDurationFkID",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDays_CustomerSubscriptions_CustomerSubscriptionFkID",
                table: "CustomerDays");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDays_Day_DayFkID",
                table: "CustomerDays");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSubscriptionLocation_CustomerSubscriptions_CustomerSubscriptionFkID",
                table: "CustomerSubscriptionLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSubscriptionLocation_ParkingLocations_ParkingLocationsFkID",
                table: "CustomerSubscriptionLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSubscriptions_Customer_CustomerFkID",
                table: "CustomerSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSubscriptions_PaymentTypes_PaymentTypeFkID",
                table: "CustomerSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSubscriptions_SubscriptionDurations_SubscriptionDurationFkID",
                table: "CustomerSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerVehicles_Customer_CustomerFkID",
                table: "CustomerVehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPrivilage_UserGroup",
                table: "GroupPrivilage");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPrivilage_FormPages",
                table: "GroupPrivilage");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingImages_Parking",
                table: "ParkingImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingLocations_Area",
                table: "ParkingLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingLocations_Parking",
                table: "ParkingLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_PromoCode_ParkingLocations_ParkingLocationsFkID",
                table: "PromoCode");

            migrationBuilder.DropForeignKey(
                name: "FK_Siteline_ParkingLocations",
                table: "Siteline");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Parking_ParkingFkID",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionDurations_Subscription_SubscriptionFkId",
                table: "SubscriptionDurations");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_CollectionTransaction_PaymentSource_PaymentSourceId",
                table: "Table_CollectionTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_Tariff_ParkingLocations",
                table: "Table_Tariff");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_TransactionDetail_ParkingLocations_ParkingLocationsId",
                table: "Table_TransactionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_TransactionDetail_PaymentTypes_PaymentTypeId",
                table: "Table_TransactionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_TransactionDetail_TransactionStatus",
                table: "Table_TransactionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPrivilage_FormPages",
                table: "UserPrivilage");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPrivilage_AdminUsers",
                table: "UserPrivilage");

            migrationBuilder.DropIndex(
                name: "IX_UserPrivilage_PageID_FK",
                table: "UserPrivilage");

            migrationBuilder.DropIndex(
                name: "IX_UserPrivilage_UserID_FK",
                table: "UserPrivilage");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionDurations_SubscriptionFkId",
                table: "SubscriptionDurations");

            migrationBuilder.DropIndex(
                name: "IX_Subscription_ParkingFkID",
                table: "Subscription");

            migrationBuilder.DropIndex(
                name: "IX_Siteline_ParkingLocation_FK_ID",
                table: "Siteline");

            migrationBuilder.DropIndex(
                name: "IX_PromoCode_ParkingLocationsFkID",
                table: "PromoCode");

            migrationBuilder.DropIndex(
                name: "IX_ParkingLocations_Area_FK_ID",
                table: "ParkingLocations");

            migrationBuilder.DropIndex(
                name: "IX_ParkingLocations_Parking_FK_ID",
                table: "ParkingLocations");

            migrationBuilder.DropIndex(
                name: "IX_ParkingImages_Parking_FK_ID",
                table: "ParkingImages");

            migrationBuilder.DropIndex(
                name: "IX_GroupPrivilage_GroupID_FK",
                table: "GroupPrivilage");

            migrationBuilder.DropIndex(
                name: "IX_GroupPrivilage_PageID_FK",
                table: "GroupPrivilage");

            migrationBuilder.DropIndex(
                name: "IX_CustomerVehicles_CustomerFkID",
                table: "CustomerVehicles");

            migrationBuilder.DropIndex(
                name: "IX_CustomerSubscriptions_CustomerFkID",
                table: "CustomerSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CustomerSubscriptions_PaymentTypeFkID",
                table: "CustomerSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CustomerSubscriptions_SubscriptionDurationFkID",
                table: "CustomerSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CustomerSubscriptionLocation_CustomerSubscriptionFkID",
                table: "CustomerSubscriptionLocation");

            migrationBuilder.DropIndex(
                name: "IX_CustomerSubscriptionLocation_ParkingLocationsFkID",
                table: "CustomerSubscriptionLocation");

            migrationBuilder.DropIndex(
                name: "IX_CustomerDays_CustomerSubscriptionFkID",
                table: "CustomerDays");

            migrationBuilder.DropIndex(
                name: "IX_CustomerDays_DayFkID",
                table: "CustomerDays");

            migrationBuilder.DropIndex(
                name: "IX_Customer_NationalityFkID",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_ParkingFkID",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_SubscriptionDurationFkID",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_AdminUsers_Parking_FK_ID",
                table: "AdminUsers");

            migrationBuilder.DropIndex(
                name: "IX_AdminUsers_UserType_FK_Id",
                table: "AdminUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Table_TransactionDetail",
                table: "Table_TransactionDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Table_Tariff",
                table: "Table_Tariff");

            migrationBuilder.DropIndex(
                name: "IX_Table_Tariff_PrkingLocation_FK_ID",
                table: "Table_Tariff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Table_CollectionTransaction",
                table: "Table_CollectionTransaction");

            migrationBuilder.DropColumn(
                name: "PageID_FK",
                table: "UserPrivilage");

            migrationBuilder.DropColumn(
                name: "UserID_FK",
                table: "UserPrivilage");

            migrationBuilder.DropColumn(
                name: "SubscriptionFkId",
                table: "SubscriptionDurations");

            migrationBuilder.DropColumn(
                name: "ParkingFkID",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "ParkingLocation_FK_ID",
                table: "Siteline");

            migrationBuilder.DropColumn(
                name: "ParkingLocationsFkID",
                table: "PromoCode");

            migrationBuilder.DropColumn(
                name: "Area_FK_ID",
                table: "ParkingLocations");

            migrationBuilder.DropColumn(
                name: "Parking_FK_ID",
                table: "ParkingLocations");

            migrationBuilder.DropColumn(
                name: "Parking_FK_ID",
                table: "ParkingImages");

            migrationBuilder.DropColumn(
                name: "Company_FK_ID",
                table: "Parking");

            migrationBuilder.DropColumn(
                name: "GroupID_FK",
                table: "GroupPrivilage");

            migrationBuilder.DropColumn(
                name: "PageID_FK",
                table: "GroupPrivilage");

            migrationBuilder.DropColumn(
                name: "CustomerFkID",
                table: "CustomerVehicles");

            migrationBuilder.DropColumn(
                name: "CustomerFkID",
                table: "CustomerSubscriptions");

            migrationBuilder.DropColumn(
                name: "PaymentTypeFkID",
                table: "CustomerSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionDurationFkID",
                table: "CustomerSubscriptions");

            migrationBuilder.DropColumn(
                name: "CustomerSubscriptionFkID",
                table: "CustomerSubscriptionLocation");

            migrationBuilder.DropColumn(
                name: "ParkingLocationsFkID",
                table: "CustomerSubscriptionLocation");

            migrationBuilder.DropColumn(
                name: "CustomerSubscriptionFkID",
                table: "CustomerDays");

            migrationBuilder.DropColumn(
                name: "DayFkID",
                table: "CustomerDays");

            migrationBuilder.DropColumn(
                name: "NationalityFkID",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ParkingFkID",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "SubscriptionDurationFkID",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Parking_FK_ID",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "UserType_FK_Id",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "PrkingLocation_FK_ID",
                table: "Table_Tariff");

            migrationBuilder.RenameTable(
                name: "Table_TransactionDetail",
                newName: "TableTransactionDetail");

            migrationBuilder.RenameTable(
                name: "Table_Tariff",
                newName: "TableTariff");

            migrationBuilder.RenameTable(
                name: "Table_CollectionTransaction",
                newName: "TableCollectionTransaction");

            migrationBuilder.RenameColumn(
                name: "nameEn",
                table: "UserType",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "nameAr",
                table: "UserType",
                newName: "NameAr");

            migrationBuilder.RenameColumn(
                name: "isEnable",
                table: "UserType",
                newName: "IsEnable");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "UserType",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "nameEn",
                table: "TransactionStatus",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "nameAr",
                table: "TransactionStatus",
                newName: "NameAr");

            migrationBuilder.RenameColumn(
                name: "isoCode",
                table: "TransactionStatus",
                newName: "IsoCode");

            migrationBuilder.RenameColumn(
                name: "isPublic",
                table: "TransactionStatus",
                newName: "IsPublic");

            migrationBuilder.RenameColumn(
                name: "isEnable",
                table: "TransactionStatus",
                newName: "IsEnable");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "TransactionStatus",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserID",
                table: "Siteline",
                newName: "ModifiedUserId");

            migrationBuilder.RenameColumn(
                name: "LineID",
                table: "Siteline",
                newName: "LineId");

            migrationBuilder.RenameColumn(
                name: "LineDPUName",
                table: "Siteline",
                newName: "LineDpuname");

            migrationBuilder.RenameColumn(
                name: "LineDPUID",
                table: "Siteline",
                newName: "LineDpuid");

            migrationBuilder.RenameColumn(
                name: "CreatedUserID",
                table: "Siteline",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "nameEn",
                table: "PaymentSource",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "nameAr",
                table: "PaymentSource",
                newName: "NameAr");

            migrationBuilder.RenameColumn(
                name: "isoCode",
                table: "PaymentSource",
                newName: "IsoCode");

            migrationBuilder.RenameColumn(
                name: "isPublic",
                table: "PaymentSource",
                newName: "IsPublic");

            migrationBuilder.RenameColumn(
                name: "isEnable",
                table: "PaymentSource",
                newName: "IsEnable");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "PaymentSource",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "SiteID",
                table: "ParkingLocations",
                newName: "SiteId");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserID",
                table: "ParkingLocations",
                newName: "ModifiedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUserID",
                table: "ParkingLocations",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "allowedtimeperminute",
                table: "ParkingLocations",
                newName: "Allowedtimeperminute");

            migrationBuilder.RenameColumn(
                name: "Gps_Lat",
                table: "ParkingLocations",
                newName: "GpsLat");

            migrationBuilder.RenameColumn(
                name: "path",
                table: "ParkingImages",
                newName: "Path");

            migrationBuilder.RenameColumn(
                name: "orderNo",
                table: "ParkingImages",
                newName: "OrderNo");

            migrationBuilder.RenameColumn(
                name: "isMain",
                table: "ParkingImages",
                newName: "IsMain");

            migrationBuilder.RenameColumn(
                name: "isEnable",
                table: "ParkingImages",
                newName: "IsEnable");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "ParkingImages",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserID",
                table: "Parking",
                newName: "ModifiedUserId");

            migrationBuilder.RenameColumn(
                name: "invoicelogo",
                table: "Parking",
                newName: "Invoicelogo");

            migrationBuilder.RenameColumn(
                name: "CreatedUserID",
                table: "Parking",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "remark",
                table: "AuditTrialid",
                newName: "Remark");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "AuditTrialid",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "controllerName",
                table: "AuditTrialid",
                newName: "ControllerName");

            migrationBuilder.RenameColumn(
                name: "actionName",
                table: "AuditTrialid",
                newName: "ActionName");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "AuditTrialid",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "AdminUsers",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "AdminUsers",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "mobile",
                table: "AdminUsers",
                newName: "Mobile");

            migrationBuilder.RenameColumn(
                name: "isEnable",
                table: "AdminUsers",
                newName: "IsEnable");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "AdminUsers",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "AdminUsers",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "fullname",
                table: "AdminUsers",
                newName: "Fullname");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "AdminUsers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "createddate",
                table: "AdminUsers",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "TransactionID",
                table: "TableTransactionDetail",
                newName: "TransactionId");

            migrationBuilder.RenameColumn(
                name: "DPUName",
                table: "TableTransactionDetail",
                newName: "Dpuname");

            migrationBuilder.RenameColumn(
                name: "DPUId",
                table: "TableTransactionDetail",
                newName: "Dpuid");

            migrationBuilder.RenameColumn(
                name: "Status_FK_ID",
                table: "TableTransactionDetail",
                newName: "StatusFkId");

            migrationBuilder.RenameIndex(
                name: "IX_Table_TransactionDetail_Status_FK_ID",
                table: "TableTransactionDetail",
                newName: "IX_TableTransactionDetail_StatusFkId");

            migrationBuilder.RenameIndex(
                name: "IX_Table_TransactionDetail_PaymentTypeId",
                table: "TableTransactionDetail",
                newName: "IX_TableTransactionDetail_PaymentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Table_TransactionDetail_ParkingLocationsId",
                table: "TableTransactionDetail",
                newName: "IX_TableTransactionDetail_ParkingLocationsId");

            migrationBuilder.RenameColumn(
                name: "nameEn",
                table: "TableTariff",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "isEnable",
                table: "TableTariff",
                newName: "IsEnable");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "TableTariff",
                newName: "IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Table_CollectionTransaction_PaymentSourceId",
                table: "TableCollectionTransaction",
                newName: "IX_TableCollectionTransaction_PaymentSourceId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedDate",
                table: "UserPrivilage",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "UserPrivilage",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdminUsersId",
                table: "UserPrivilage",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FormPagesId",
                table: "UserPrivilage",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedDate",
                table: "UserGroup",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "UserGroup",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                table: "SubscriptionDurations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingId",
                table: "Subscription",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Siteline",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Siteline",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingLocationId",
                table: "Siteline",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingLocationsID",
                table: "PromoCode",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedDate",
                table: "ParkingLocations",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ParkingLocations",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "ParkingLocations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingId",
                table: "ParkingLocations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingId",
                table: "ParkingImages",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Parking",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Parking",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedDate",
                table: "GroupPrivilage",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "GroupPrivilage",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "GroupPrivilage",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupIdNavigationId",
                table: "GroupPrivilage",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageId",
                table: "GroupPrivilage",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageIdNavigationId",
                table: "GroupPrivilage",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedDate",
                table: "FormPages",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "FormPages",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "CustomerVehicles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "CustomerSubscriptions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentTypeId",
                table: "CustomerSubscriptions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionDurationId",
                table: "CustomerSubscriptions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerSubscriptionID",
                table: "CustomerSubscriptionLocation",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingLocationsID",
                table: "CustomerSubscriptionLocation",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerSubscriptionID",
                table: "CustomerDays",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayID",
                table: "CustomerDays",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NationalityId",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingId",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionDurationId",
                table: "Customer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AuditTrialid",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingId",
                table: "AdminUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                table: "AdminUsers",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionTime",
                table: "TableTransactionDetail",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReciveDate",
                table: "TableTransactionDetail",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenDate",
                table: "TableTransactionDetail",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TableTransactionDetail",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CloseDate",
                table: "TableTransactionDetail",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingLocationId",
                table: "TableTariff",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeOut",
                table: "TableCollectionTransaction",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeIn",
                table: "TableCollectionTransaction",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenDate",
                table: "TableCollectionTransaction",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "TableCollectionTransaction",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CloseDate",
                table: "TableCollectionTransaction",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableTransactionDetail",
                table: "TableTransactionDetail",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableTariff",
                table: "TableTariff",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableCollectionTransaction",
                table: "TableCollectionTransaction",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserPrivilage_AdminUsersId",
                table: "UserPrivilage",
                column: "AdminUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPrivilage_FormPagesId",
                table: "UserPrivilage",
                column: "FormPagesId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionDurations_SubscriptionId",
                table: "SubscriptionDurations",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_ParkingId",
                table: "Subscription",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Siteline_ParkingLocationId",
                table: "Siteline",
                column: "ParkingLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCode_ParkingLocationsID",
                table: "PromoCode",
                column: "ParkingLocationsID");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingLocations_AreaId",
                table: "ParkingLocations",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingLocations_ParkingId",
                table: "ParkingLocations",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingImages_ParkingId",
                table: "ParkingImages",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPrivilage_GroupIdNavigationId",
                table: "GroupPrivilage",
                column: "GroupIdNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPrivilage_PageIdNavigationId",
                table: "GroupPrivilage",
                column: "PageIdNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVehicles_CustomerId",
                table: "CustomerVehicles",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSubscriptions_CustomerId",
                table: "CustomerSubscriptions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSubscriptions_PaymentTypeId",
                table: "CustomerSubscriptions",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSubscriptions_SubscriptionDurationId",
                table: "CustomerSubscriptions",
                column: "SubscriptionDurationId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSubscriptionLocation_CustomerSubscriptionID",
                table: "CustomerSubscriptionLocation",
                column: "CustomerSubscriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSubscriptionLocation_ParkingLocationsID",
                table: "CustomerSubscriptionLocation",
                column: "ParkingLocationsID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDays_CustomerSubscriptionID",
                table: "CustomerDays",
                column: "CustomerSubscriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDays_DayID",
                table: "CustomerDays",
                column: "DayID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_NationalityId",
                table: "Customer",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ParkingId",
                table: "Customer",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_SubscriptionDurationId",
                table: "Customer",
                column: "SubscriptionDurationId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_ParkingId",
                table: "AdminUsers",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_UserTypeId",
                table: "AdminUsers",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TableTariff_ParkingLocationId",
                table: "TableTariff",
                column: "ParkingLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminUsers_Parking_ParkingId",
                table: "AdminUsers",
                column: "ParkingId",
                principalTable: "Parking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdminUsers_UserType_UserTypeId",
                table: "AdminUsers",
                column: "UserTypeId",
                principalTable: "UserType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Nationality_NationalityId",
                table: "Customer",
                column: "NationalityId",
                principalTable: "Nationality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Parking_ParkingId",
                table: "Customer",
                column: "ParkingId",
                principalTable: "Parking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_SubscriptionDurations_SubscriptionDurationId",
                table: "Customer",
                column: "SubscriptionDurationId",
                principalTable: "SubscriptionDurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDays_CustomerSubscriptions_CustomerSubscriptionID",
                table: "CustomerDays",
                column: "CustomerSubscriptionID",
                principalTable: "CustomerSubscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDays_Day_DayID",
                table: "CustomerDays",
                column: "DayID",
                principalTable: "Day",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSubscriptionLocation_CustomerSubscriptions_CustomerSubscriptionID",
                table: "CustomerSubscriptionLocation",
                column: "CustomerSubscriptionID",
                principalTable: "CustomerSubscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSubscriptionLocation_ParkingLocations_ParkingLocationsID",
                table: "CustomerSubscriptionLocation",
                column: "ParkingLocationsID",
                principalTable: "ParkingLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSubscriptions_Customer_CustomerId",
                table: "CustomerSubscriptions",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSubscriptions_PaymentTypes_PaymentTypeId",
                table: "CustomerSubscriptions",
                column: "PaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSubscriptions_SubscriptionDurations_SubscriptionDurationId",
                table: "CustomerSubscriptions",
                column: "SubscriptionDurationId",
                principalTable: "SubscriptionDurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerVehicles_Customer_CustomerId",
                table: "CustomerVehicles",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingImages_Parking_ParkingId",
                table: "ParkingImages",
                column: "ParkingId",
                principalTable: "Parking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingLocations_Area_AreaId",
                table: "ParkingLocations",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingLocations_Parking_ParkingId",
                table: "ParkingLocations",
                column: "ParkingId",
                principalTable: "Parking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCode_ParkingLocations_ParkingLocationsID",
                table: "PromoCode",
                column: "ParkingLocationsID",
                principalTable: "ParkingLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Siteline_ParkingLocations_ParkingLocationId",
                table: "Siteline",
                column: "ParkingLocationId",
                principalTable: "ParkingLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Parking_ParkingId",
                table: "Subscription",
                column: "ParkingId",
                principalTable: "Parking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionDurations_Subscription_SubscriptionId",
                table: "SubscriptionDurations",
                column: "SubscriptionId",
                principalTable: "Subscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TableCollectionTransaction_PaymentSource_PaymentSourceId",
                table: "TableCollectionTransaction",
                column: "PaymentSourceId",
                principalTable: "PaymentSource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TableTariff_ParkingLocations_ParkingLocationId",
                table: "TableTariff",
                column: "ParkingLocationId",
                principalTable: "ParkingLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TableTransactionDetail_ParkingLocations_ParkingLocationsId",
                table: "TableTransactionDetail",
                column: "ParkingLocationsId",
                principalTable: "ParkingLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TableTransactionDetail_PaymentTypes_PaymentTypeId",
                table: "TableTransactionDetail",
                column: "PaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TableTransactionDetail_TransactionStatus_StatusFkId",
                table: "TableTransactionDetail",
                column: "StatusFkId",
                principalTable: "TransactionStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPrivilage_AdminUsers_AdminUsersId",
                table: "UserPrivilage",
                column: "AdminUsersId",
                principalTable: "AdminUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPrivilage_FormPages_FormPagesId",
                table: "UserPrivilage",
                column: "FormPagesId",
                principalTable: "FormPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminUsers_Parking_ParkingId",
                table: "AdminUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AdminUsers_UserType_UserTypeId",
                table: "AdminUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Nationality_NationalityId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Parking_ParkingId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_SubscriptionDurations_SubscriptionDurationId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDays_CustomerSubscriptions_CustomerSubscriptionID",
                table: "CustomerDays");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDays_Day_DayID",
                table: "CustomerDays");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSubscriptionLocation_CustomerSubscriptions_CustomerSubscriptionID",
                table: "CustomerSubscriptionLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSubscriptionLocation_ParkingLocations_ParkingLocationsID",
                table: "CustomerSubscriptionLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSubscriptions_Customer_CustomerId",
                table: "CustomerSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSubscriptions_PaymentTypes_PaymentTypeId",
                table: "CustomerSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSubscriptions_SubscriptionDurations_SubscriptionDurationId",
                table: "CustomerSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerVehicles_Customer_CustomerId",
                table: "CustomerVehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPrivilage_UserGroup_GroupIdNavigationId",
                table: "GroupPrivilage");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupPrivilage_FormPages_PageIdNavigationId",
                table: "GroupPrivilage");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingImages_Parking_ParkingId",
                table: "ParkingImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingLocations_Area_AreaId",
                table: "ParkingLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingLocations_Parking_ParkingId",
                table: "ParkingLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_PromoCode_ParkingLocations_ParkingLocationsID",
                table: "PromoCode");

            migrationBuilder.DropForeignKey(
                name: "FK_Siteline_ParkingLocations_ParkingLocationId",
                table: "Siteline");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Parking_ParkingId",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionDurations_Subscription_SubscriptionId",
                table: "SubscriptionDurations");

            migrationBuilder.DropForeignKey(
                name: "FK_TableCollectionTransaction_PaymentSource_PaymentSourceId",
                table: "TableCollectionTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_TableTariff_ParkingLocations_ParkingLocationId",
                table: "TableTariff");

            migrationBuilder.DropForeignKey(
                name: "FK_TableTransactionDetail_ParkingLocations_ParkingLocationsId",
                table: "TableTransactionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TableTransactionDetail_PaymentTypes_PaymentTypeId",
                table: "TableTransactionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TableTransactionDetail_TransactionStatus_StatusFkId",
                table: "TableTransactionDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPrivilage_AdminUsers_AdminUsersId",
                table: "UserPrivilage");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPrivilage_FormPages_FormPagesId",
                table: "UserPrivilage");

            migrationBuilder.DropIndex(
                name: "IX_UserPrivilage_AdminUsersId",
                table: "UserPrivilage");

            migrationBuilder.DropIndex(
                name: "IX_UserPrivilage_FormPagesId",
                table: "UserPrivilage");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionDurations_SubscriptionId",
                table: "SubscriptionDurations");

            migrationBuilder.DropIndex(
                name: "IX_Subscription_ParkingId",
                table: "Subscription");

            migrationBuilder.DropIndex(
                name: "IX_Siteline_ParkingLocationId",
                table: "Siteline");

            migrationBuilder.DropIndex(
                name: "IX_PromoCode_ParkingLocationsID",
                table: "PromoCode");

            migrationBuilder.DropIndex(
                name: "IX_ParkingLocations_AreaId",
                table: "ParkingLocations");

            migrationBuilder.DropIndex(
                name: "IX_ParkingLocations_ParkingId",
                table: "ParkingLocations");

            migrationBuilder.DropIndex(
                name: "IX_ParkingImages_ParkingId",
                table: "ParkingImages");

            migrationBuilder.DropIndex(
                name: "IX_GroupPrivilage_GroupIdNavigationId",
                table: "GroupPrivilage");

            migrationBuilder.DropIndex(
                name: "IX_GroupPrivilage_PageIdNavigationId",
                table: "GroupPrivilage");

            migrationBuilder.DropIndex(
                name: "IX_CustomerVehicles_CustomerId",
                table: "CustomerVehicles");

            migrationBuilder.DropIndex(
                name: "IX_CustomerSubscriptions_CustomerId",
                table: "CustomerSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CustomerSubscriptions_PaymentTypeId",
                table: "CustomerSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CustomerSubscriptions_SubscriptionDurationId",
                table: "CustomerSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_CustomerSubscriptionLocation_CustomerSubscriptionID",
                table: "CustomerSubscriptionLocation");

            migrationBuilder.DropIndex(
                name: "IX_CustomerSubscriptionLocation_ParkingLocationsID",
                table: "CustomerSubscriptionLocation");

            migrationBuilder.DropIndex(
                name: "IX_CustomerDays_CustomerSubscriptionID",
                table: "CustomerDays");

            migrationBuilder.DropIndex(
                name: "IX_CustomerDays_DayID",
                table: "CustomerDays");

            migrationBuilder.DropIndex(
                name: "IX_Customer_NationalityId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_ParkingId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_SubscriptionDurationId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_AdminUsers_ParkingId",
                table: "AdminUsers");

            migrationBuilder.DropIndex(
                name: "IX_AdminUsers_UserTypeId",
                table: "AdminUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableTransactionDetail",
                table: "TableTransactionDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableTariff",
                table: "TableTariff");

            migrationBuilder.DropIndex(
                name: "IX_TableTariff_ParkingLocationId",
                table: "TableTariff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableCollectionTransaction",
                table: "TableCollectionTransaction");

            migrationBuilder.DropColumn(
                name: "AdminUsersId",
                table: "UserPrivilage");

            migrationBuilder.DropColumn(
                name: "FormPagesId",
                table: "UserPrivilage");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "SubscriptionDurations");

            migrationBuilder.DropColumn(
                name: "ParkingId",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "ParkingLocationId",
                table: "Siteline");

            migrationBuilder.DropColumn(
                name: "ParkingLocationsID",
                table: "PromoCode");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "ParkingLocations");

            migrationBuilder.DropColumn(
                name: "ParkingId",
                table: "ParkingLocations");

            migrationBuilder.DropColumn(
                name: "ParkingId",
                table: "ParkingImages");

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

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CustomerVehicles");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CustomerSubscriptions");

            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                table: "CustomerSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionDurationId",
                table: "CustomerSubscriptions");

            migrationBuilder.DropColumn(
                name: "CustomerSubscriptionID",
                table: "CustomerSubscriptionLocation");

            migrationBuilder.DropColumn(
                name: "ParkingLocationsID",
                table: "CustomerSubscriptionLocation");

            migrationBuilder.DropColumn(
                name: "CustomerSubscriptionID",
                table: "CustomerDays");

            migrationBuilder.DropColumn(
                name: "DayID",
                table: "CustomerDays");

            migrationBuilder.DropColumn(
                name: "NationalityId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ParkingId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "SubscriptionDurationId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ParkingId",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "ParkingLocationId",
                table: "TableTariff");

            migrationBuilder.RenameTable(
                name: "TableTransactionDetail",
                newName: "Table_TransactionDetail");

            migrationBuilder.RenameTable(
                name: "TableTariff",
                newName: "Table_Tariff");

            migrationBuilder.RenameTable(
                name: "TableCollectionTransaction",
                newName: "Table_CollectionTransaction");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "UserType",
                newName: "nameEn");

            migrationBuilder.RenameColumn(
                name: "NameAr",
                table: "UserType",
                newName: "nameAr");

            migrationBuilder.RenameColumn(
                name: "IsEnable",
                table: "UserType",
                newName: "isEnable");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "UserType",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "TransactionStatus",
                newName: "nameEn");

            migrationBuilder.RenameColumn(
                name: "NameAr",
                table: "TransactionStatus",
                newName: "nameAr");

            migrationBuilder.RenameColumn(
                name: "IsoCode",
                table: "TransactionStatus",
                newName: "isoCode");

            migrationBuilder.RenameColumn(
                name: "IsPublic",
                table: "TransactionStatus",
                newName: "isPublic");

            migrationBuilder.RenameColumn(
                name: "IsEnable",
                table: "TransactionStatus",
                newName: "isEnable");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "TransactionStatus",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserId",
                table: "Siteline",
                newName: "ModifiedUserID");

            migrationBuilder.RenameColumn(
                name: "LineId",
                table: "Siteline",
                newName: "LineID");

            migrationBuilder.RenameColumn(
                name: "LineDpuname",
                table: "Siteline",
                newName: "LineDPUName");

            migrationBuilder.RenameColumn(
                name: "LineDpuid",
                table: "Siteline",
                newName: "LineDPUID");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "Siteline",
                newName: "CreatedUserID");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "PaymentSource",
                newName: "nameEn");

            migrationBuilder.RenameColumn(
                name: "NameAr",
                table: "PaymentSource",
                newName: "nameAr");

            migrationBuilder.RenameColumn(
                name: "IsoCode",
                table: "PaymentSource",
                newName: "isoCode");

            migrationBuilder.RenameColumn(
                name: "IsPublic",
                table: "PaymentSource",
                newName: "isPublic");

            migrationBuilder.RenameColumn(
                name: "IsEnable",
                table: "PaymentSource",
                newName: "isEnable");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "PaymentSource",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "SiteId",
                table: "ParkingLocations",
                newName: "SiteID");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserId",
                table: "ParkingLocations",
                newName: "ModifiedUserID");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "ParkingLocations",
                newName: "CreatedUserID");

            migrationBuilder.RenameColumn(
                name: "Allowedtimeperminute",
                table: "ParkingLocations",
                newName: "allowedtimeperminute");

            migrationBuilder.RenameColumn(
                name: "GpsLat",
                table: "ParkingLocations",
                newName: "Gps_Lat");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "ParkingImages",
                newName: "path");

            migrationBuilder.RenameColumn(
                name: "OrderNo",
                table: "ParkingImages",
                newName: "orderNo");

            migrationBuilder.RenameColumn(
                name: "IsMain",
                table: "ParkingImages",
                newName: "isMain");

            migrationBuilder.RenameColumn(
                name: "IsEnable",
                table: "ParkingImages",
                newName: "isEnable");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "ParkingImages",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserId",
                table: "Parking",
                newName: "ModifiedUserID");

            migrationBuilder.RenameColumn(
                name: "Invoicelogo",
                table: "Parking",
                newName: "invoicelogo");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "Parking",
                newName: "CreatedUserID");

            migrationBuilder.RenameColumn(
                name: "Remark",
                table: "AuditTrialid",
                newName: "remark");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "AuditTrialid",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "ControllerName",
                table: "AuditTrialid",
                newName: "controllerName");

            migrationBuilder.RenameColumn(
                name: "ActionName",
                table: "AuditTrialid",
                newName: "actionName");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "AuditTrialid",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "AdminUsers",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "AdminUsers",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Mobile",
                table: "AdminUsers",
                newName: "mobile");

            migrationBuilder.RenameColumn(
                name: "IsEnable",
                table: "AdminUsers",
                newName: "isEnable");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "AdminUsers",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "AdminUsers",
                newName: "image");

            migrationBuilder.RenameColumn(
                name: "Fullname",
                table: "AdminUsers",
                newName: "fullname");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "AdminUsers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "AdminUsers",
                newName: "createddate");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Table_TransactionDetail",
                newName: "TransactionID");

            migrationBuilder.RenameColumn(
                name: "Dpuname",
                table: "Table_TransactionDetail",
                newName: "DPUName");

            migrationBuilder.RenameColumn(
                name: "Dpuid",
                table: "Table_TransactionDetail",
                newName: "DPUId");

            migrationBuilder.RenameColumn(
                name: "StatusFkId",
                table: "Table_TransactionDetail",
                newName: "Status_FK_ID");

            migrationBuilder.RenameIndex(
                name: "IX_TableTransactionDetail_StatusFkId",
                table: "Table_TransactionDetail",
                newName: "IX_Table_TransactionDetail_Status_FK_ID");

            migrationBuilder.RenameIndex(
                name: "IX_TableTransactionDetail_PaymentTypeId",
                table: "Table_TransactionDetail",
                newName: "IX_Table_TransactionDetail_PaymentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TableTransactionDetail_ParkingLocationsId",
                table: "Table_TransactionDetail",
                newName: "IX_Table_TransactionDetail_ParkingLocationsId");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Table_Tariff",
                newName: "nameEn");

            migrationBuilder.RenameColumn(
                name: "IsEnable",
                table: "Table_Tariff",
                newName: "isEnable");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Table_Tariff",
                newName: "isDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_TableCollectionTransaction_PaymentSourceId",
                table: "Table_CollectionTransaction",
                newName: "IX_Table_CollectionTransaction_PaymentSourceId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedDate",
                table: "UserPrivilage",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "UserPrivilage",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageID_FK",
                table: "UserPrivilage",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID_FK",
                table: "UserPrivilage",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedDate",
                table: "UserGroup",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "UserGroup",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionFkId",
                table: "SubscriptionDurations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingFkID",
                table: "Subscription",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Siteline",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Siteline",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingLocation_FK_ID",
                table: "Siteline",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingLocationsFkID",
                table: "PromoCode",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedDate",
                table: "ParkingLocations",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ParkingLocations",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Area_FK_ID",
                table: "ParkingLocations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Parking_FK_ID",
                table: "ParkingLocations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Parking_FK_ID",
                table: "ParkingImages",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Parking",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Parking",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Company_FK_ID",
                table: "Parking",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedDate",
                table: "GroupPrivilage",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "GroupPrivilage",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupID_FK",
                table: "GroupPrivilage",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageID_FK",
                table: "GroupPrivilage",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedDate",
                table: "FormPages",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "FormPages",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerFkID",
                table: "CustomerVehicles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerFkID",
                table: "CustomerSubscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentTypeFkID",
                table: "CustomerSubscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionDurationFkID",
                table: "CustomerSubscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerSubscriptionFkID",
                table: "CustomerSubscriptionLocation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingLocationsFkID",
                table: "CustomerSubscriptionLocation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerSubscriptionFkID",
                table: "CustomerDays",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayFkID",
                table: "CustomerDays",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NationalityFkID",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkingFkID",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionDurationFkID",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AuditTrialid",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Parking_FK_ID",
                table: "AdminUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserType_FK_Id",
                table: "AdminUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionTime",
                table: "Table_TransactionDetail",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReciveDate",
                table: "Table_TransactionDetail",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenDate",
                table: "Table_TransactionDetail",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Table_TransactionDetail",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CloseDate",
                table: "Table_TransactionDetail",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrkingLocation_FK_ID",
                table: "Table_Tariff",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeOut",
                table: "Table_CollectionTransaction",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeIn",
                table: "Table_CollectionTransaction",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenDate",
                table: "Table_CollectionTransaction",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Table_CollectionTransaction",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CloseDate",
                table: "Table_CollectionTransaction",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Table_TransactionDetail",
                table: "Table_TransactionDetail",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Table_Tariff",
                table: "Table_Tariff",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Table_CollectionTransaction",
                table: "Table_CollectionTransaction",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserPrivilage_PageID_FK",
                table: "UserPrivilage",
                column: "PageID_FK");

            migrationBuilder.CreateIndex(
                name: "IX_UserPrivilage_UserID_FK",
                table: "UserPrivilage",
                column: "UserID_FK");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionDurations_SubscriptionFkId",
                table: "SubscriptionDurations",
                column: "SubscriptionFkId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_ParkingFkID",
                table: "Subscription",
                column: "ParkingFkID");

            migrationBuilder.CreateIndex(
                name: "IX_Siteline_ParkingLocation_FK_ID",
                table: "Siteline",
                column: "ParkingLocation_FK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCode_ParkingLocationsFkID",
                table: "PromoCode",
                column: "ParkingLocationsFkID");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingLocations_Area_FK_ID",
                table: "ParkingLocations",
                column: "Area_FK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingLocations_Parking_FK_ID",
                table: "ParkingLocations",
                column: "Parking_FK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingImages_Parking_FK_ID",
                table: "ParkingImages",
                column: "Parking_FK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPrivilage_GroupID_FK",
                table: "GroupPrivilage",
                column: "GroupID_FK");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPrivilage_PageID_FK",
                table: "GroupPrivilage",
                column: "PageID_FK");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerVehicles_CustomerFkID",
                table: "CustomerVehicles",
                column: "CustomerFkID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSubscriptions_CustomerFkID",
                table: "CustomerSubscriptions",
                column: "CustomerFkID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSubscriptions_PaymentTypeFkID",
                table: "CustomerSubscriptions",
                column: "PaymentTypeFkID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSubscriptions_SubscriptionDurationFkID",
                table: "CustomerSubscriptions",
                column: "SubscriptionDurationFkID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSubscriptionLocation_CustomerSubscriptionFkID",
                table: "CustomerSubscriptionLocation",
                column: "CustomerSubscriptionFkID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSubscriptionLocation_ParkingLocationsFkID",
                table: "CustomerSubscriptionLocation",
                column: "ParkingLocationsFkID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDays_CustomerSubscriptionFkID",
                table: "CustomerDays",
                column: "CustomerSubscriptionFkID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDays_DayFkID",
                table: "CustomerDays",
                column: "DayFkID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_NationalityFkID",
                table: "Customer",
                column: "NationalityFkID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ParkingFkID",
                table: "Customer",
                column: "ParkingFkID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_SubscriptionDurationFkID",
                table: "Customer",
                column: "SubscriptionDurationFkID");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_Parking_FK_ID",
                table: "AdminUsers",
                column: "Parking_FK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_UserType_FK_Id",
                table: "AdminUsers",
                column: "UserType_FK_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Table_Tariff_PrkingLocation_FK_ID",
                table: "Table_Tariff",
                column: "PrkingLocation_FK_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminUsers_Parking",
                table: "AdminUsers",
                column: "Parking_FK_ID",
                principalTable: "Parking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdminUsers_UserType",
                table: "AdminUsers",
                column: "UserType_FK_Id",
                principalTable: "UserType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Nationality_NationalityFkID",
                table: "Customer",
                column: "NationalityFkID",
                principalTable: "Nationality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Parking_ParkingFkID",
                table: "Customer",
                column: "ParkingFkID",
                principalTable: "Parking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_SubscriptionDurations_SubscriptionDurationFkID",
                table: "Customer",
                column: "SubscriptionDurationFkID",
                principalTable: "SubscriptionDurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDays_CustomerSubscriptions_CustomerSubscriptionFkID",
                table: "CustomerDays",
                column: "CustomerSubscriptionFkID",
                principalTable: "CustomerSubscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDays_Day_DayFkID",
                table: "CustomerDays",
                column: "DayFkID",
                principalTable: "Day",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSubscriptionLocation_CustomerSubscriptions_CustomerSubscriptionFkID",
                table: "CustomerSubscriptionLocation",
                column: "CustomerSubscriptionFkID",
                principalTable: "CustomerSubscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSubscriptionLocation_ParkingLocations_ParkingLocationsFkID",
                table: "CustomerSubscriptionLocation",
                column: "ParkingLocationsFkID",
                principalTable: "ParkingLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSubscriptions_Customer_CustomerFkID",
                table: "CustomerSubscriptions",
                column: "CustomerFkID",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSubscriptions_PaymentTypes_PaymentTypeFkID",
                table: "CustomerSubscriptions",
                column: "PaymentTypeFkID",
                principalTable: "PaymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSubscriptions_SubscriptionDurations_SubscriptionDurationFkID",
                table: "CustomerSubscriptions",
                column: "SubscriptionDurationFkID",
                principalTable: "SubscriptionDurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerVehicles_Customer_CustomerFkID",
                table: "CustomerVehicles",
                column: "CustomerFkID",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPrivilage_UserGroup",
                table: "GroupPrivilage",
                column: "GroupID_FK",
                principalTable: "UserGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPrivilage_FormPages",
                table: "GroupPrivilage",
                column: "PageID_FK",
                principalTable: "FormPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingImages_Parking",
                table: "ParkingImages",
                column: "Parking_FK_ID",
                principalTable: "Parking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingLocations_Area",
                table: "ParkingLocations",
                column: "Area_FK_ID",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingLocations_Parking",
                table: "ParkingLocations",
                column: "Parking_FK_ID",
                principalTable: "Parking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCode_ParkingLocations_ParkingLocationsFkID",
                table: "PromoCode",
                column: "ParkingLocationsFkID",
                principalTable: "ParkingLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Siteline_ParkingLocations",
                table: "Siteline",
                column: "ParkingLocation_FK_ID",
                principalTable: "ParkingLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Parking_ParkingFkID",
                table: "Subscription",
                column: "ParkingFkID",
                principalTable: "Parking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionDurations_Subscription_SubscriptionFkId",
                table: "SubscriptionDurations",
                column: "SubscriptionFkId",
                principalTable: "Subscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Table_CollectionTransaction_PaymentSource_PaymentSourceId",
                table: "Table_CollectionTransaction",
                column: "PaymentSourceId",
                principalTable: "PaymentSource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Table_Tariff_ParkingLocations",
                table: "Table_Tariff",
                column: "PrkingLocation_FK_ID",
                principalTable: "ParkingLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Table_TransactionDetail_ParkingLocations_ParkingLocationsId",
                table: "Table_TransactionDetail",
                column: "ParkingLocationsId",
                principalTable: "ParkingLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Table_TransactionDetail_PaymentTypes_PaymentTypeId",
                table: "Table_TransactionDetail",
                column: "PaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Table_TransactionDetail_TransactionStatus",
                table: "Table_TransactionDetail",
                column: "Status_FK_ID",
                principalTable: "TransactionStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPrivilage_FormPages",
                table: "UserPrivilage",
                column: "PageID_FK",
                principalTable: "FormPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPrivilage_AdminUsers",
                table: "UserPrivilage",
                column: "UserID_FK",
                principalTable: "AdminUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
