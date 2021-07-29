using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UTCAPPCMS.DAL.Migrations
{
    public partial class models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Day",
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
                    SortNo = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Day", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationality",
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
                    SortNo = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationality", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromoCode",
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
                    Code = table.Column<string>(nullable: true),
                    NoOfUse = table.Column<int>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: true),
                    ToDate = table.Column<DateTime>(nullable: true),
                    ParkingLocationsFkID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromoCode_ParkingLocations_ParkingLocationsFkID",
                        column: x => x.ParkingLocationsFkID,
                        principalTable: "ParkingLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
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
                    Name = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ParkingFkID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscription_Parking_ParkingFkID",
                        column: x => x.ParkingFkID,
                        principalTable: "Parking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionDurations",
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
                    durationInDays = table.Column<int>(nullable: true),
                    DaysCount = table.Column<int>(nullable: true),
                    HoursCount = table.Column<int>(nullable: true),
                    VechicleCount = table.Column<int>(nullable: true),
                    Price = table.Column<double>(nullable: true),
                    PriceMulti = table.Column<double>(nullable: true),
                    AllDays = table.Column<bool>(nullable: true),
                    AllTime = table.Column<bool>(nullable: true),
                    IsMullti = table.Column<bool>(nullable: true),
                    SubscriptionFkId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionDurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionDurations_Subscription_SubscriptionFkId",
                        column: x => x.SubscriptionFkId,
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
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
                    FullName = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    Derpartment = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    StafNo = table.Column<string>(nullable: true),
                    WorkPosition = table.Column<string>(nullable: true),
                    CivilID = table.Column<string>(nullable: true),
                    ActivationCode = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    IsActivated = table.Column<bool>(nullable: true),
                    NationalityFkID = table.Column<int>(nullable: true),
                    ParkingFkID = table.Column<int>(nullable: true),
                    SubscriptionDurationFkID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Nationality_NationalityFkID",
                        column: x => x.NationalityFkID,
                        principalTable: "Nationality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_Parking_ParkingFkID",
                        column: x => x.ParkingFkID,
                        principalTable: "Parking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_SubscriptionDurations_SubscriptionDurationFkID",
                        column: x => x.SubscriptionDurationFkID,
                        principalTable: "SubscriptionDurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSubscriptions",
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
                    PaymentReference = table.Column<string>(nullable: true),
                    ActivatedDate = table.Column<DateTime>(nullable: true),
                    PaymentDate = table.Column<DateTime>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: true),
                    ToDate = table.Column<DateTime>(nullable: true),
                    FromHours = table.Column<TimeSpan>(nullable: true),
                    ToHours = table.Column<TimeSpan>(nullable: true),
                    Price = table.Column<double>(nullable: true),
                    IsActivated = table.Column<bool>(nullable: true),
                    IsPaid = table.Column<bool>(nullable: true),
                    CustomerFkID = table.Column<int>(nullable: true),
                    SubscriptionDurationFkID = table.Column<int>(nullable: true),
                    PaymentTypeFkID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerSubscriptions_Customer_CustomerFkID",
                        column: x => x.CustomerFkID,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSubscriptions_PaymentTypes_PaymentTypeFkID",
                        column: x => x.PaymentTypeFkID,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSubscriptions_SubscriptionDurations_SubscriptionDurationFkID",
                        column: x => x.SubscriptionDurationFkID,
                        principalTable: "SubscriptionDurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerVechicles",
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
                    Image = table.Column<string>(nullable: true),
                    VechicleType = table.Column<string>(nullable: true),
                    IsVip = table.Column<bool>(nullable: true),
                    CustomerFkID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerVechicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerVechicles_Customer_CustomerFkID",
                        column: x => x.CustomerFkID,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDays",
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
                    CustomerSubscriptionFkID = table.Column<int>(nullable: true),
                    DayFkID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerDays_CustomerSubscriptions_CustomerSubscriptionFkID",
                        column: x => x.CustomerSubscriptionFkID,
                        principalTable: "CustomerSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerDays_Day_DayFkID",
                        column: x => x.DayFkID,
                        principalTable: "Day",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSubscriptionLocation",
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
                    CustomerSubscriptionFkID = table.Column<int>(nullable: true),
                    ParkingLocationsFkID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSubscriptionLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerSubscriptionLocation_CustomerSubscriptions_CustomerSubscriptionFkID",
                        column: x => x.CustomerSubscriptionFkID,
                        principalTable: "CustomerSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerSubscriptionLocation_ParkingLocations_ParkingLocationsFkID",
                        column: x => x.ParkingLocationsFkID,
                        principalTable: "ParkingLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_CustomerDays_CustomerSubscriptionFkID",
                table: "CustomerDays",
                column: "CustomerSubscriptionFkID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDays_DayFkID",
                table: "CustomerDays",
                column: "DayFkID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSubscriptionLocation_CustomerSubscriptionFkID",
                table: "CustomerSubscriptionLocation",
                column: "CustomerSubscriptionFkID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSubscriptionLocation_ParkingLocationsFkID",
                table: "CustomerSubscriptionLocation",
                column: "ParkingLocationsFkID");

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
                name: "IX_CustomerVechicles_CustomerFkID",
                table: "CustomerVechicles",
                column: "CustomerFkID");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCode_ParkingLocationsFkID",
                table: "PromoCode",
                column: "ParkingLocationsFkID");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_ParkingFkID",
                table: "Subscription",
                column: "ParkingFkID");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionDurations_SubscriptionFkId",
                table: "SubscriptionDurations",
                column: "SubscriptionFkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerDays");

            migrationBuilder.DropTable(
                name: "CustomerSubscriptionLocation");

            migrationBuilder.DropTable(
                name: "CustomerVechicles");

            migrationBuilder.DropTable(
                name: "PromoCode");

            migrationBuilder.DropTable(
                name: "Day");

            migrationBuilder.DropTable(
                name: "CustomerSubscriptions");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Nationality");

            migrationBuilder.DropTable(
                name: "SubscriptionDurations");

            migrationBuilder.DropTable(
                name: "Subscription");
        }
    }
}
