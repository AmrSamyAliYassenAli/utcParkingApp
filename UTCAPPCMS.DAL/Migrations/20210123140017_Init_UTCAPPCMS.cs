using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UTCAPPCMS.DAL.Migrations
{
    public partial class Init_UTCAPPCMS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditTrialid",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    createdBy = table.Column<int>(nullable: true),
                    ModifiedUserId = table.Column<int>(nullable: true),
                    IsEnable = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    controllerName = table.Column<string>(nullable: true),
                    actionName = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrialid", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormPages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUserId = table.Column<int>(nullable: true),
                    ModifiedUserId = table.Column<int>(nullable: true),
                    IsEnable = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    FormKey = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormPages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parking",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUserID = table.Column<int>(nullable: true),
                    ModifiedUserID = table.Column<int>(nullable: true),
                    IsEnable = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ReferanceKey = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true),
                    AddressEn = table.Column<string>(nullable: true),
                    AddressAr = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    ContactEmail = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Company_FK_ID = table.Column<int>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    invoicelogo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parking", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentSource",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<int>(nullable: true),
                    ModifiedUserId = table.Column<int>(nullable: true),
                    isEnable = table.Column<bool>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: true),
                    nameEn = table.Column<string>(nullable: true),
                    nameAr = table.Column<string>(nullable: true),
                    isoCode = table.Column<string>(nullable: true),
                    isPublic = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentSource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<int>(nullable: true),
                    ModifiedUserId = table.Column<int>(nullable: true),
                    isEnable = table.Column<bool>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: true),
                    nameEn = table.Column<string>(nullable: true),
                    nameAr = table.Column<string>(nullable: true),
                    isoCode = table.Column<string>(nullable: true),
                    isPublic = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUserId = table.Column<int>(nullable: true),
                    ModifiedUserId = table.Column<int>(nullable: true),
                    IsEnable = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<int>(nullable: true),
                    ModifiedUserId = table.Column<int>(nullable: true),
                    isEnable = table.Column<bool>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: true),
                    nameEn = table.Column<string>(nullable: true),
                    nameAr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkingImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<int>(nullable: true),
                    ModifiedUserId = table.Column<int>(nullable: true),
                    isEnable = table.Column<bool>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: true),
                    path = table.Column<string>(nullable: true),
                    orderNo = table.Column<int>(nullable: true),
                    Parking_FK_ID = table.Column<int>(nullable: true),
                    isMain = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingImages_Parking",
                        column: x => x.Parking_FK_ID,
                        principalTable: "Parking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParkingLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUserID = table.Column<int>(nullable: true),
                    ModifiedUserID = table.Column<int>(nullable: true),
                    IsEnable = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    SiteName = table.Column<string>(nullable: true),
                    SiteNo = table.Column<string>(nullable: true),
                    SiteID = table.Column<string>(nullable: true),
                    Qrcode = table.Column<string>(nullable: true),
                    Parking_FK_ID = table.Column<int>(nullable: true),
                    Area_FK_ID = table.Column<int>(nullable: true),
                    Block = table.Column<string>(nullable: true),
                    Gps_Lat = table.Column<string>(nullable: true),
                    IsPublic = table.Column<bool>(nullable: true),
                    CarCapacity = table.Column<int>(nullable: true),
                    FloorsNo = table.Column<int>(nullable: true),
                    AddressEn = table.Column<string>(nullable: true),
                    AddressAr = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    ContactEmail = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    OpenFromTime = table.Column<TimeSpan>(nullable: true),
                    OpenToTime = table.Column<TimeSpan>(nullable: true),
                    allowedtimeperminute = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingLocations_Parking",
                        column: x => x.Parking_FK_ID,
                        principalTable: "Parking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Table_CollectionTransaction",
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
                    PlateNumber = table.Column<string>(nullable: true),
                    InTransactionId = table.Column<int>(nullable: true),
                    OutTransactionId = table.Column<int>(nullable: true),
                    TimeIn = table.Column<DateTime>(type: "datetime", nullable: true),
                    TimeOut = table.Column<DateTime>(type: "datetime", nullable: true),
                    Duration = table.Column<int>(nullable: true),
                    DurationText = table.Column<string>(nullable: true),
                    TotalMount = table.Column<double>(nullable: true),
                    Paid = table.Column<double>(nullable: true),
                    Remaining = table.Column<double>(nullable: true),
                    OpenDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CloseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClosedUserId = table.Column<int>(nullable: true),
                    LineNumber = table.Column<int>(nullable: true),
                    IsCollected = table.Column<bool>(nullable: true),
                    ShiftId = table.Column<int>(nullable: true),
                    paymentsource_FK_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_CollectionTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Table_CollectionTransaction_PaymentSource",
                        column: x => x.paymentsource_FK_ID,
                        principalTable: "PaymentSource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Table_TransactionDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<int>(nullable: true),
                    ModifiedUserId = table.Column<int>(nullable: true),
                    IsEnable = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    TransactionID = table.Column<long>(nullable: true),
                    RecordVersionId = table.Column<long>(nullable: true),
                    SiteId = table.Column<long>(nullable: true),
                    SiteName = table.Column<string>(nullable: true),
                    DPUId = table.Column<long>(nullable: true),
                    DPUName = table.Column<string>(nullable: true),
                    TransactionTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    PlateNumber = table.Column<string>(nullable: true),
                    PlatePrefix = table.Column<string>(nullable: true),
                    PlateNumberOnly = table.Column<string>(nullable: true),
                    PlateCity = table.Column<string>(nullable: true),
                    PlateCountry = table.Column<string>(nullable: true),
                    Confidence = table.Column<string>(nullable: true),
                    PlateType = table.Column<string>(nullable: true),
                    PlateCategory = table.Column<string>(nullable: true),
                    PlateColor = table.Column<string>(nullable: true),
                    AlarmStatusCode = table.Column<string>(nullable: true),
                    AlarmStatusTitle = table.Column<string>(nullable: true),
                    LaneNo = table.Column<int>(nullable: true),
                    LaneName = table.Column<string>(nullable: true),
                    VehicleBrand = table.Column<string>(nullable: true),
                    VehicleModel = table.Column<string>(nullable: true),
                    VehicleColor = table.Column<int>(nullable: true),
                    VehicleClassification = table.Column<int>(nullable: true),
                    Status_FK_ID = table.Column<int>(nullable: true),
                    ReciveDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    OpenDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CloseDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_TransactionDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Table_TransactionDetail_TransactionStatus",
                        column: x => x.Status_FK_ID,
                        principalTable: "TransactionStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupPrivilage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUserId = table.Column<int>(nullable: true),
                    ModifiedUserId = table.Column<int>(nullable: true),
                    IsEnable = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    GroupID_FK = table.Column<int>(nullable: true),
                    PageID_FK = table.Column<int>(nullable: true),
                    View = table.Column<bool>(nullable: true),
                    Add = table.Column<bool>(nullable: true),
                    Edit = table.Column<bool>(nullable: true),
                    Delete = table.Column<bool>(nullable: true),
                    Search = table.Column<bool>(nullable: true),
                    Print = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPrivilage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupPrivilage_UserGroup",
                        column: x => x.GroupID_FK,
                        principalTable: "UserGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupPrivilage_FormPages",
                        column: x => x.PageID_FK,
                        principalTable: "FormPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createddate = table.Column<DateTime>(fixedLength: true, maxLength: 10, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<int>(nullable: true),
                    ModifiedUserId = table.Column<int>(nullable: true),
                    isEnable = table.Column<bool>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: true),
                    fullname = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    mobile = table.Column<string>(nullable: true),
                    username = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    image = table.Column<string>(nullable: true),
                    Parking_FK_ID = table.Column<int>(nullable: true),
                    UserType_FK_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminUsers_Parking",
                        column: x => x.Parking_FK_ID,
                        principalTable: "Parking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdminUsers_UserType",
                        column: x => x.UserType_FK_Id,
                        principalTable: "UserType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Siteline",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUserID = table.Column<int>(nullable: true),
                    ModifiedUserID = table.Column<int>(nullable: true),
                    IsEnable = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    LineName = table.Column<string>(nullable: true),
                    LineID = table.Column<string>(nullable: true),
                    LineNumber = table.Column<int>(nullable: true),
                    LineType = table.Column<string>(nullable: true),
                    LineDPUID = table.Column<string>(nullable: true),
                    LineDPUName = table.Column<string>(nullable: true),
                    ParkingLocation_FK_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siteline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Siteline_ParkingLocations",
                        column: x => x.ParkingLocation_FK_ID,
                        principalTable: "ParkingLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Table_Tariff",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<int>(nullable: true),
                    ModifiedUserId = table.Column<int>(nullable: true),
                    isEnable = table.Column<bool>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: true),
                    nameEn = table.Column<string>(nullable: true),
                    HourPrice = table.Column<double>(nullable: true),
                    PrkingLocation_FK_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_Tariff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Table_Tariff_ParkingLocations",
                        column: x => x.PrkingLocation_FK_ID,
                        principalTable: "ParkingLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPrivilage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUserId = table.Column<int>(nullable: true),
                    ModifiedUserId = table.Column<int>(nullable: true),
                    IsEnable = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    UserID_FK = table.Column<int>(nullable: true),
                    PageID_FK = table.Column<int>(nullable: true),
                    View = table.Column<bool>(nullable: true),
                    Add = table.Column<bool>(nullable: true),
                    Edit = table.Column<bool>(nullable: true),
                    Delete = table.Column<bool>(nullable: true),
                    Search = table.Column<bool>(nullable: true),
                    Print = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPrivilage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPrivilage_FormPages",
                        column: x => x.PageID_FK,
                        principalTable: "FormPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPrivilage_AdminUsers",
                        column: x => x.UserID_FK,
                        principalTable: "AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_Parking_FK_ID",
                table: "AdminUsers",
                column: "Parking_FK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_UserType_FK_Id",
                table: "AdminUsers",
                column: "UserType_FK_Id");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPrivilage_GroupID_FK",
                table: "GroupPrivilage",
                column: "GroupID_FK");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPrivilage_PageID_FK",
                table: "GroupPrivilage",
                column: "PageID_FK");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingImages_Parking_FK_ID",
                table: "ParkingImages",
                column: "Parking_FK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingLocations_Parking_FK_ID",
                table: "ParkingLocations",
                column: "Parking_FK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Siteline_ParkingLocation_FK_ID",
                table: "Siteline",
                column: "ParkingLocation_FK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Table_CollectionTransaction_paymentsource_FK_ID",
                table: "Table_CollectionTransaction",
                column: "paymentsource_FK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Table_Tariff_PrkingLocation_FK_ID",
                table: "Table_Tariff",
                column: "PrkingLocation_FK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Table_TransactionDetail_Status_FK_ID",
                table: "Table_TransactionDetail",
                column: "Status_FK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPrivilage_PageID_FK",
                table: "UserPrivilage",
                column: "PageID_FK");

            migrationBuilder.CreateIndex(
                name: "IX_UserPrivilage_UserID_FK",
                table: "UserPrivilage",
                column: "UserID_FK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTrialid");

            migrationBuilder.DropTable(
                name: "GroupPrivilage");

            migrationBuilder.DropTable(
                name: "ParkingImages");

            migrationBuilder.DropTable(
                name: "Siteline");

            migrationBuilder.DropTable(
                name: "Table_CollectionTransaction");

            migrationBuilder.DropTable(
                name: "Table_Tariff");

            migrationBuilder.DropTable(
                name: "Table_TransactionDetail");

            migrationBuilder.DropTable(
                name: "UserPrivilage");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "PaymentSource");

            migrationBuilder.DropTable(
                name: "ParkingLocations");

            migrationBuilder.DropTable(
                name: "TransactionStatus");

            migrationBuilder.DropTable(
                name: "FormPages");

            migrationBuilder.DropTable(
                name: "AdminUsers");

            migrationBuilder.DropTable(
                name: "Parking");

            migrationBuilder.DropTable(
                name: "UserType");
        }
    }
}
