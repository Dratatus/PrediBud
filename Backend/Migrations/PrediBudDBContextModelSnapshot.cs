﻿// <auto-generated />
using System;
using Backend.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(PrediBudDBContext))]
    partial class PrediBudDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
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

            modelBuilder.Entity("Backend.Data.Models.Common.Address", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Backend.Data.Models.Constructions.Specyfication.ConstructionSpecification", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("ConstructionSpecifications", (string)null);

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Backend.Data.Models.Notifications.ConstructionOrderNotification", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("ConstructionOrderID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ClientId");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SupplierID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
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

                    b.Property<decimal?>("AgreedPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BannedWorkerIds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<decimal?>("ClientProposedPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ConstructionSpecificationId")
                        .HasColumnType("int");

                    b.Property<int>("ConstructionType")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RequestedStartTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("WorkerId")
                        .HasColumnType("int");

                    b.Property<decimal?>("WorkerProposedPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("placementPhotos")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ClientId");

                    b.HasIndex("ConstructionSpecificationId")
                        .IsUnique();

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ClientID")
                        .HasColumnType("int");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<string>("Pics")
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

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("ID");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("UserType").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Backend.Data.Models.Constructions.Dimensions.Balcony.BalconySpecification", b =>
                {
                    b.HasBaseType("Backend.Data.Models.Constructions.Specyfication.ConstructionSpecification");

                    b.Property<decimal>("Length")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RailingMaterial")
                        .HasColumnType("int");

                    b.Property<decimal>("Width")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("ConstructionSpecifications", t =>
                        {
                            t.Property("Width")
                                .HasColumnName("BalconySpecification_Width");
                        });

                    b.HasDiscriminator().HasValue(11);
                });

            modelBuilder.Entity("Backend.Data.Models.Constructions.Dimensions.Doors.DoorsSpecification", b =>
                {
                    b.HasBaseType("Backend.Data.Models.Constructions.Specyfication.ConstructionSpecification");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<decimal>("Height")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Material")
                        .HasColumnType("int");

                    b.Property<decimal>("Width")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("ConstructionSpecifications", t =>
                        {
                            t.Property("Amount")
                                .HasColumnName("DoorsSpecification_Amount");

                            t.Property("Height")
                                .HasColumnName("DoorsSpecification_Height");

                            t.Property("Material")
                                .HasColumnName("DoorsSpecification_Material");

                            t.Property("Width")
                                .HasColumnName("DoorsSpecification_Width");
                        });

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("Backend.Data.Models.Constructions.Dimensions.Facade.FacadeSpecification", b =>
                {
                    b.HasBaseType("Backend.Data.Models.Constructions.Specyfication.ConstructionSpecification");

                    b.Property<int>("FinishMaterial")
                        .HasColumnType("int");

                    b.Property<int>("InsulationType")
                        .HasColumnType("int");

                    b.Property<decimal>("SurfaceArea")
                        .HasColumnType("decimal(18,2)");

                    b.HasDiscriminator().HasValue(4);
                });

            modelBuilder.Entity("Backend.Data.Models.Constructions.Dimensions.Floor.FlooringSpecification", b =>
                {
                    b.HasBaseType("Backend.Data.Models.Constructions.Specyfication.ConstructionSpecification");

                    b.Property<decimal>("Area")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Material")
                        .HasColumnType("int");

                    b.ToTable("ConstructionSpecifications", t =>
                        {
                            t.Property("Area")
                                .HasColumnName("FlooringSpecification_Area");

                            t.Property("Material")
                                .HasColumnName("FlooringSpecification_Material");
                        });

                    b.HasDiscriminator().HasValue(5);
                });

            modelBuilder.Entity("Backend.Data.Models.Constructions.Dimensions.InsulationOfAtticSpecification", b =>
                {
                    b.HasBaseType("Backend.Data.Models.Constructions.Specyfication.ConstructionSpecification");

                    b.Property<decimal>("Area")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Material")
                        .HasColumnType("int");

                    b.Property<decimal>("Thickness")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("ConstructionSpecifications", t =>
                        {
                            t.Property("Area")
                                .HasColumnName("InsulationOfAtticSpecification_Area");

                            t.Property("Material")
                                .HasColumnName("InsulationOfAtticSpecification_Material");
                        });

                    b.HasDiscriminator().HasValue(7);
                });

            modelBuilder.Entity("Backend.Data.Models.Constructions.Dimensions.WindowsSpecification", b =>
                {
                    b.HasBaseType("Backend.Data.Models.Constructions.Specyfication.ConstructionSpecification");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<decimal>("Height")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Width")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("ConstructionSpecifications", t =>
                        {
                            t.Property("Height")
                                .HasColumnName("WindowsSpecification_Height");
                        });

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Backend.Data.Models.Constructions.Specyfication.Ceiling.SuspendedCeilingSpecification", b =>
                {
                    b.HasBaseType("Backend.Data.Models.Constructions.Specyfication.ConstructionSpecification");

                    b.Property<decimal>("Area")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Height")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Material")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue(6);
                });

            modelBuilder.Entity("Backend.Data.Models.Constructions.Specyfication.Painting.PaintingSpecification", b =>
                {
                    b.HasBaseType("Backend.Data.Models.Constructions.Specyfication.ConstructionSpecification");

                    b.Property<int>("NumberOfCoats")
                        .HasColumnType("int");

                    b.Property<int>("PaintType")
                        .HasColumnType("int");

                    b.Property<decimal>("WallSurfaceArea")
                        .HasColumnType("decimal(18,2)");

                    b.HasDiscriminator().HasValue(9);
                });

            modelBuilder.Entity("Backend.Data.Models.Constructions.Specyfication.Plastering.PlasteringSpecification", b =>
                {
                    b.HasBaseType("Backend.Data.Models.Constructions.Specyfication.ConstructionSpecification");

                    b.Property<int>("PlasterType")
                        .HasColumnType("int");

                    b.Property<decimal>("WallSurfaceArea")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("ConstructionSpecifications", t =>
                        {
                            t.Property("WallSurfaceArea")
                                .HasColumnName("PlasteringSpecification_WallSurfaceArea");
                        });

                    b.HasDiscriminator().HasValue(8);
                });

            modelBuilder.Entity("Backend.Data.Models.Constructions.Specyfication.ShellOpen.ShellOpenSpecification", b =>
                {
                    b.HasBaseType("Backend.Data.Models.Constructions.Specyfication.ConstructionSpecification");

                    b.Property<decimal?>("CeilingArea")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CeilingMaterial")
                        .HasColumnType("int");

                    b.Property<int?>("ChimneyCount")
                        .HasColumnType("int");

                    b.Property<decimal?>("FoundationDepth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("FoundationLength")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("FoundationWidth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ImagesUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("LoadBearingWallHeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("LoadBearingWallMaterial")
                        .HasColumnType("int");

                    b.Property<decimal?>("LoadBearingWallThickness")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("LoadBearingWallWidth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("PartitionWallHeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PartitionWallMaterial")
                        .HasColumnType("int");

                    b.Property<decimal?>("PartitionWallThickness")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("PartitionWallWidth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("RoofArea")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RoofMaterial")
                        .HasColumnType("int");

                    b.Property<decimal?>("RoofPitch")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("VentilationSystemCount")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue(12);
                });

            modelBuilder.Entity("Backend.Data.Models.Constructions.Specyfication.Stairs.StaircaseSpecification", b =>
                {
                    b.HasBaseType("Backend.Data.Models.Constructions.Specyfication.ConstructionSpecification");

                    b.Property<decimal>("Height")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Material")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfSteps")
                        .HasColumnType("int");

                    b.Property<decimal>("Width")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("ConstructionSpecifications", t =>
                        {
                            t.Property("Height")
                                .HasColumnName("StaircaseSpecification_Height");

                            t.Property("Material")
                                .HasColumnName("StaircaseSpecification_Material");

                            t.Property("Width")
                                .HasColumnName("StaircaseSpecification_Width");
                        });

                    b.HasDiscriminator().HasValue(10);
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
                    b.HasOne("Backend.Data.Models.Users.Client", "Client")
                        .WithMany("ConstructionOrderNotifications")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Backend.Data.Models.Orders.ConstructionOrder", null)
                        .WithMany("Notifications")
                        .HasForeignKey("ConstructionOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Data.Models.Users.Worker", "Worker")
                        .WithMany("ConstructionOrderNotifications")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Client");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Backend.Data.Models.Notifications.MaterialNotification", b =>
                {
                    b.HasOne("Backend.Data.Models.Suppliers.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierID");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Backend.Data.Models.Orders.ConstructionOrder", b =>
                {
                    b.HasOne("Backend.Data.Models.Users.Client", "Client")
                        .WithMany("ConstructionOrders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Data.Models.Constructions.Specyfication.ConstructionSpecification", "ConstructionSpecification")
                        .WithOne()
                        .HasForeignKey("Backend.Data.Models.Orders.ConstructionOrder", "ConstructionSpecificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Data.Models.Users.Worker", "Worker")
                        .WithMany("AssignedOrders")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Client");

                    b.Navigation("ConstructionSpecification");

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
                    b.HasOne("Backend.Data.Models.Common.Address", "Address")
                        .WithOne()
                        .HasForeignKey("Backend.Data.Models.Users.User", "AddressId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("Backend.Data.Models.Credidentials.Credentials", "Credentials", b1 =>
                        {
                            b1.Property<int>("UserID")
                                .HasColumnType("int");

                            b1.Property<string>("Email")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PasswordHash")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserID");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserID");
                        });

                    b.Navigation("Address");

                    b.Navigation("Credentials");
                });

            modelBuilder.Entity("Backend.Data.Models.Users.Client", b =>
                {
                    b.OwnsOne("Backend.Data.Models.Common.ContactDetails", "ContactDetails", b1 =>
                        {
                            b1.Property<int>("ClientID")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Phone")
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ClientID");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("ClientID");
                        });

                    b.Navigation("ContactDetails");
                });

            modelBuilder.Entity("Backend.Data.Models.Users.Worker", b =>
                {
                    b.OwnsOne("Backend.Data.Models.Common.ContactDetails", "ContactDetails", b1 =>
                        {
                            b1.Property<int>("WorkerID")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Phone")
                                .ValueGeneratedOnUpdateSometimes()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("WorkerID");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("WorkerID");
                        });

                    b.Navigation("ContactDetails");
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
                    b.Navigation("ConstructionOrderNotifications");

                    b.Navigation("ConstructionOrders");

                    b.Navigation("MaterialOrders");
                });

            modelBuilder.Entity("Backend.Data.Models.Users.Worker", b =>
                {
                    b.Navigation("AssignedOrders");

                    b.Navigation("ConstructionOrderNotifications");
                });
#pragma warning restore 612, 618
        }
    }
}
