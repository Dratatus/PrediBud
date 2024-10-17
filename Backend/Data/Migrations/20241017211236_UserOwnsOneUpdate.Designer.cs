﻿// <auto-generated />
using System;
using Backend.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Data.Migrations
{
    [DbContext(typeof(PrediBudDBContext))]
    [Migration("20241017211236_UserOwnsOneUpdate")]
    partial class UserOwnsOneUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Backend.Data.Models.Calculation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Dimensions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Taxes")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TypeOfStructure")
                        .HasColumnType("int");

                    b.Property<decimal>("UserPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("MaterialId");

                    b.ToTable("Calculations");
                });

            modelBuilder.Entity("Backend.Data.Models.Notifications.ConstructionOrderNotification", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("ConstructionOrderID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ConstructionOrderID");

                    b.HasIndex("WorkerId");

                    b.ToTable("ConstructionOrderNotifications");
                });

            modelBuilder.Entity("Backend.Data.Models.Notifications.MaterialNotification", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("SupplierID");

                    b.ToTable("MaterialNotifications");
                });

            modelBuilder.Entity("Backend.Data.Models.Orders.ConstructionOrder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ClientId");

                    b.HasIndex("WorkerId");

                    b.ToTable("ConstructionOrders");
                });

            modelBuilder.Entity("Backend.Data.Models.Suppliers.Material", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PriceWithTaxes")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PriceWithoutTaxes")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SupplierId");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("Backend.Data.Models.Suppliers.MaterialOrder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ClientID")
                        .HasColumnType("int");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<string>("Pics")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("ClientID");

                    b.HasIndex("MaterialId");

                    b.ToTable("MaterialOrders");
                });

            modelBuilder.Entity("Backend.Data.Models.Suppliers.Supplier", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Backend.Data.Models.Users.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("ID");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("UserType").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Backend.Data.Models.Users.Client", b =>
                {
                    b.HasBaseType("Backend.Data.Models.Users.User");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("Backend.Data.Models.Users.Worker", b =>
                {
                    b.HasBaseType("Backend.Data.Models.Users.User");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Worker");
                });

            modelBuilder.Entity("Backend.Data.Models.Calculation", b =>
                {
                    b.HasOne("Backend.Data.Models.Suppliers.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");
                });

            modelBuilder.Entity("Backend.Data.Models.Notifications.ConstructionOrderNotification", b =>
                {
                    b.HasOne("Backend.Data.Models.Orders.ConstructionOrder", null)
                        .WithMany("Notifications")
                        .HasForeignKey("ConstructionOrderID");

                    b.HasOne("Backend.Data.Models.Users.Worker", "Worker")
                        .WithMany("ConstructionOrderNotifications")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Backend.Data.Models.Notifications.MaterialNotification", b =>
                {
                    b.HasOne("Backend.Data.Models.Suppliers.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Backend.Data.Models.Orders.ConstructionOrder", b =>
                {
                    b.HasOne("Backend.Data.Models.Users.Client", "Client")
                        .WithMany("ConstructionOrders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Data.Models.Users.Worker", "Worker")
                        .WithMany("ConstructionOrders")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Client");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Backend.Data.Models.Suppliers.Material", b =>
                {
                    b.HasOne("Backend.Data.Models.Suppliers.Supplier", "Supplier")
                        .WithMany("AvailableMaterials")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Backend.Data.Models.Suppliers.MaterialOrder", b =>
                {
                    b.HasOne("Backend.Data.Models.Users.Client", null)
                        .WithMany("MaterialOrders")
                        .HasForeignKey("ClientID");

                    b.HasOne("Backend.Data.Models.Suppliers.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");
                });

            modelBuilder.Entity("Backend.Data.Models.Users.User", b =>
                {
                    b.OwnsOne("Backend.Data.Models.Credidentials.Credentials", "Credentials", b1 =>
                        {
                            b1.Property<int>("UserID")
                                .HasColumnType("int");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PasswordHash")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserID");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserID");
                        });

                    b.Navigation("Credentials")
                        .IsRequired();
                });

            modelBuilder.Entity("Backend.Data.Models.Users.Client", b =>
                {
                    b.OwnsOne("Backend.Data.Models.Common.ContactDetails", "ContactDetails", b1 =>
                        {
                            b1.Property<int>("ClientID")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Phone")
                                .IsRequired()
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ClientID");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("ClientID");
                        });

                    b.Navigation("ContactDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("Backend.Data.Models.Users.Worker", b =>
                {
                    b.OwnsOne("Backend.Data.Models.Common.ContactDetails", "ContactDetails", b1 =>
                        {
                            b1.Property<int>("WorkerID")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Phone")
                                .IsRequired()
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("WorkerID");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("WorkerID");
                        });

                    b.Navigation("ContactDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("Backend.Data.Models.Orders.ConstructionOrder", b =>
                {
                    b.Navigation("Notifications");
                });

            modelBuilder.Entity("Backend.Data.Models.Suppliers.Supplier", b =>
                {
                    b.Navigation("AvailableMaterials");
                });

            modelBuilder.Entity("Backend.Data.Models.Users.Client", b =>
                {
                    b.Navigation("ConstructionOrders");

                    b.Navigation("MaterialOrders");
                });

            modelBuilder.Entity("Backend.Data.Models.Users.Worker", b =>
                {
                    b.Navigation("ConstructionOrderNotifications");

                    b.Navigation("ConstructionOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
