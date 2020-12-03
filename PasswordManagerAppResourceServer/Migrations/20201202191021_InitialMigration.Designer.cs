﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PasswordManagerAppResourceServer.Models;

namespace PasswordManagerAppResourceServer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201202191021_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AddressName")
                        .IsRequired()
                        .HasColumnName("address_name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnName("city")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("PersonalInfoId")
                        .HasColumnName("personal_info_id")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnName("street")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("StreetNumber")
                        .IsRequired()
                        .HasColumnName("street_number")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnName("zip_code")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("PersonalInfoId");

                    b.ToTable("address");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.CreditCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CardHolderName")
                        .IsRequired()
                        .HasColumnName("cardholder_name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnName("card_number")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ExpirationDate")
                        .IsRequired()
                        .HasColumnName("expiration_date")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("SecurityCode")
                        .IsRequired()
                        .HasColumnName("security_code")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("credit_card");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.LoginData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Compromised")
                        .HasColumnName("compromised")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnName("login")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("OutOfDate")
                        .HasColumnName("out-of-date")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.Property<string>("Website")
                        .HasColumnName("website")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("login_data");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnName("Details")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("note");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.PaypalAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Compromised")
                        .HasColumnName("compromised")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnName("modified_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("paypall_account");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.PersonalInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnName("date_of_birth")
                        .HasColumnType("Date");

                    b.Property<string>("LastName")
                        .HasColumnName("last_name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("SecondName")
                        .HasColumnName("second_name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("personal_info");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.PhoneNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnName("nickname")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("PersonalInfoId")
                        .HasColumnName("personal_info_id")
                        .HasColumnType("int");

                    b.Property<string>("TelNumber")
                        .IsRequired()
                        .HasColumnName("phone_number")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnName("type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("PersonalInfoId");

                    b.ToTable("phone_number");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.SharedLoginData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnName("end_date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("LoginDataId")
                        .HasColumnName("login_data_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnName("start_date")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LoginDataId");

                    b.HasIndex("UserId");

                    b.ToTable("shared_login_data");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.Totp_user", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Create_date")
                        .HasColumnName("create_date")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Expire_date")
                        .HasColumnName("expire_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnName("token")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Totp_Users");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AuthenticationTime")
                        .HasColumnName("authentication_time")
                        .HasColumnType("int");

                    b.Property<int>("Compromised")
                        .HasColumnName("compromised")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("master_password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("PasswordNotifications")
                        .HasColumnName("password_notifications")
                        .HasColumnType("int");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnName("password_salt")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("TwoFactorAuthorization")
                        .HasColumnName("two_factor_authorization")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.UserDevice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Authorized")
                        .HasColumnName("authorized")
                        .HasColumnType("int");

                    b.Property<string>("DeviceGuid")
                        .IsRequired()
                        .HasColumnName("device_guid")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnName("ip_address")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("user_device");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.VisitorAgent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Browser")
                        .IsRequired()
                        .HasColumnName("browser")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnName("country")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("OperatingSystem")
                        .IsRequired()
                        .HasColumnName("operating_system")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("VisitTime")
                        .IsRequired()
                        .HasColumnName("visit_time")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("visitor_agent");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.Address", b =>
                {
                    b.HasOne("PasswordManagerAppResourceServer.Models.PersonalInfo", "PersonalInfo")
                        .WithMany("Addresses")
                        .HasForeignKey("PersonalInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.CreditCard", b =>
                {
                    b.HasOne("PasswordManagerAppResourceServer.Models.User", "User")
                        .WithMany("CreditCards")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.LoginData", b =>
                {
                    b.HasOne("PasswordManagerAppResourceServer.Models.User", "User")
                        .WithMany("LoginDatas")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.Note", b =>
                {
                    b.HasOne("PasswordManagerAppResourceServer.Models.User", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.PaypalAccount", b =>
                {
                    b.HasOne("PasswordManagerAppResourceServer.Models.User", "User")
                        .WithMany("PaypallAcounts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.PersonalInfo", b =>
                {
                    b.HasOne("PasswordManagerAppResourceServer.Models.User", "User")
                        .WithOne("PersonalInfo")
                        .HasForeignKey("PasswordManagerAppResourceServer.Models.PersonalInfo", "UserId");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.PhoneNumber", b =>
                {
                    b.HasOne("PasswordManagerAppResourceServer.Models.PersonalInfo", "PersonalInfo")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("PersonalInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.SharedLoginData", b =>
                {
                    b.HasOne("PasswordManagerAppResourceServer.Models.LoginData", "LoginData")
                        .WithMany("SharedLoginDatas")
                        .HasForeignKey("LoginDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PasswordManagerAppResourceServer.Models.User", "User")
                        .WithMany("SharedLoginDatas")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.Totp_user", b =>
                {
                    b.HasOne("PasswordManagerAppResourceServer.Models.User", "User")
                        .WithMany("Totp_Users")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("PasswordManagerAppResourceServer.Models.UserDevice", b =>
                {
                    b.HasOne("PasswordManagerAppResourceServer.Models.User", "User")
                        .WithMany("UserDevices")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}