using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class databaseupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ConstructionSpecifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BalconySpecification_Width = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RailingMaterial = table.Column<int>(type: "int", nullable: true),
                    DoorsSpecification_Amount = table.Column<int>(type: "int", nullable: true),
                    DoorsSpecification_Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DoorsSpecification_Width = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DoorsSpecification_Material = table.Column<int>(type: "int", nullable: true),
                    SurfaceArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InsulationType = table.Column<int>(type: "int", nullable: true),
                    FinishMaterial = table.Column<int>(type: "int", nullable: true),
                    FlooringSpecification_Area = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FlooringSpecification_Material = table.Column<int>(type: "int", nullable: true),
                    InsulationOfAtticSpecification_Area = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InsulationOfAtticSpecification_Material = table.Column<int>(type: "int", nullable: true),
                    Thickness = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    WindowsSpecification_Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Area = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Material = table.Column<int>(type: "int", nullable: true),
                    WallSurfaceArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaintType = table.Column<int>(type: "int", nullable: true),
                    NumberOfCoats = table.Column<int>(type: "int", nullable: true),
                    PlasteringSpecification_WallSurfaceArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PlasterType = table.Column<int>(type: "int", nullable: true),
                    FoundationLength = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FoundationWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FoundationDepth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoadBearingWallHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoadBearingWallWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoadBearingWallThickness = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoadBearingWallMaterial = table.Column<int>(type: "int", nullable: true),
                    PartitionWallHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartitionWallWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartitionWallThickness = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartitionWallMaterial = table.Column<int>(type: "int", nullable: true),
                    ChimneyCount = table.Column<int>(type: "int", nullable: true),
                    VentilationSystemCount = table.Column<int>(type: "int", nullable: true),
                    CeilingArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CeilingMaterial = table.Column<int>(type: "int", nullable: true),
                    RoofArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RoofMaterial = table.Column<int>(type: "int", nullable: true),
                    RoofPitch = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ImagesUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfSteps = table.Column<int>(type: "int", nullable: true),
                    StaircaseSpecification_Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StaircaseSpecification_Width = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StaircaseSpecification_Material = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionSpecifications", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Credentials_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Credentials_PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ContactDetails_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactDetails_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialNotifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierID = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialNotifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaterialNotifications_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceWithTaxes = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PriceWithoutTaxes = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Materials_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConstructionOrders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ConstructionType = table.Column<int>(type: "int", nullable: false),
                    placementPhotos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestedStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClientProposedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WorkerProposedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AgreedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: true),
                    ConstructionSpecificationId = table.Column<int>(type: "int", nullable: false),
                    BannedWorkerIds = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConstructionOrders_ConstructionSpecifications_ConstructionSpecificationId",
                        column: x => x.ConstructionSpecificationId,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConstructionOrders_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConstructionOrders_Users_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Calculations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfStructure = table.Column<int>(type: "int", nullable: false),
                    Dimensions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taxes = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UserPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calculations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Calculations_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialOrders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pics = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    ClientID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaterialOrders_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialOrders_Users_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ConstructionOrderNotifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ConstructionOrderID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionOrderNotifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConstructionOrderNotifications_ConstructionOrders_ConstructionOrderID",
                        column: x => x.ConstructionOrderID,
                        principalTable: "ConstructionOrders",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ConstructionOrderNotifications_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConstructionOrderNotifications_Users_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calculations_MaterialId",
                table: "Calculations",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionOrderNotifications_ClientId",
                table: "ConstructionOrderNotifications",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionOrderNotifications_ConstructionOrderID",
                table: "ConstructionOrderNotifications",
                column: "ConstructionOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionOrderNotifications_WorkerId",
                table: "ConstructionOrderNotifications",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionOrders_ClientId",
                table: "ConstructionOrders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionOrders_ConstructionSpecificationId",
                table: "ConstructionOrders",
                column: "ConstructionSpecificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionOrders_WorkerId",
                table: "ConstructionOrders",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialNotifications_SupplierID",
                table: "MaterialNotifications",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialOrders_ClientID",
                table: "MaterialOrders",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialOrders_MaterialId",
                table: "MaterialOrders",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_SupplierId",
                table: "Materials",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                column: "AddressId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calculations");

            migrationBuilder.DropTable(
                name: "ConstructionOrderNotifications");

            migrationBuilder.DropTable(
                name: "MaterialNotifications");

            migrationBuilder.DropTable(
                name: "MaterialOrders");

            migrationBuilder.DropTable(
                name: "ConstructionOrders");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "ConstructionSpecifications");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
