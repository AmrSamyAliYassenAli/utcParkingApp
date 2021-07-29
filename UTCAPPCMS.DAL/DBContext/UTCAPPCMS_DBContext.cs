using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.DAL.DBContext
{
    public partial class UTCAPPCMS_DBContext : DbContext
    {
        public UTCAPPCMS_DBContext(DbContextOptions<UTCAPPCMS_DBContext> options) : base(options) { }

        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<AdminUsers> AdminUsers { get; set; }
        public virtual DbSet<AuditTrialid> AuditTrialid { get; set; }
        public virtual DbSet<Parking> Parking { get; set; }
        public virtual DbSet<ParkingImages> ParkingImages { get; set; }
        public virtual DbSet<ParkingLocations> ParkingLocations { get; set; }
        public virtual DbSet<PaymentSource> PaymentSource { get; set; }
        public virtual DbSet<Siteline> Siteline { get; set; }
        public virtual DbSet<TableCollectionTransaction> TableCollectionTransaction { get; set; }
        public virtual DbSet<TableTariff> TableTariff { get; set; }
        public virtual DbSet<TableTransactionDetail> TableTransactionDetail { get; set; }
        public virtual DbSet<TransactionStatus> TransactionStatus { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<FormPages> FormPages { get; set; }
        public virtual DbSet<GroupPrivilage> GroupPrivilage { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }
        public virtual DbSet<UserPrivilage> UserPrivilage { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerDays> CustomerDays { get; set; }
        public virtual DbSet<CustomerSubscriptions> CustomerSubscriptions { get; set; }
        public virtual DbSet<CustomerSubscriptionLocation> CustomerSubscriptionLocation { get; set; }
        public virtual DbSet<CustomerVehicles> CustomerVehicles { get; set; }

        public virtual DbSet<Subscription> Subscription { get; set; }
        public virtual DbSet<SubscriptionDurations> SubscriptionDurations { get; set; }

        public virtual DbSet<Day> Day { get; set; }
        public virtual DbSet<Nationality> Nationality { get; set; }
        public virtual DbSet<PromoCode> PromoCode { get; set; }

        public virtual DbSet<ContactUs> ContactUs { get; set; }

        public virtual DbSet<PasswordForget> PasswordForget { get; set; }
        public virtual DbSet<ForgetPasswordAdminUser> ForgetPasswordAdminUsers { get; set; }
        public virtual DbSet<AppSetting> AppSettings { get; set; }

    }
}
