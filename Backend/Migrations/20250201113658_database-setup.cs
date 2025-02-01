using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class databasesetup : Migration
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
                    Amount = table.Column<int>(type: "int", nullable: true),
                    DoorsSpecification_Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DoorsSpecification_Material = table.Column<int>(type: "int", nullable: true),
                    SurfaceArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InsulationType = table.Column<int>(type: "int", nullable: true),
                    FinishMaterial = table.Column<int>(type: "int", nullable: true),
                    FlooringSpecification_Area = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FlooringSpecification_Material = table.Column<int>(type: "int", nullable: true),
                    InsulationOfAtticSpecification_Area = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InsulationOfAtticSpecification_Material = table.Column<int>(type: "int", nullable: true),
                    Thickness = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CeilingSpecification_Area = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CeilingSpecification_Material = table.Column<int>(type: "int", nullable: true),
                    Area = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Material = table.Column<int>(type: "int", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: true),
                    FoundationSpecification_Length = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FoundationSpecification_Width = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Depth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WallSurfaceArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaintType = table.Column<int>(type: "int", nullable: true),
                    NumberOfCoats = table.Column<int>(type: "int", nullable: true),
                    PlasteringSpecification_WallSurfaceArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PlasterType = table.Column<int>(type: "int", nullable: true),
                    RoofSpecification_Area = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RoofSpecification_Material = table.Column<int>(type: "int", nullable: true),
                    Pitch = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FoundationSpecificationID = table.Column<int>(type: "int", nullable: true),
                    LoadBearingWallMaterialID = table.Column<int>(type: "int", nullable: true),
                    PartitionWallID = table.Column<int>(type: "int", nullable: true),
                    ChimneyID = table.Column<int>(type: "int", nullable: true),
                    VentilationID = table.Column<int>(type: "int", nullable: true),
                    CellingID = table.Column<int>(type: "int", nullable: true),
                    RoofID = table.Column<int>(type: "int", nullable: true),
                    ImagesUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfSteps = table.Column<int>(type: "int", nullable: true),
                    StaircaseSpecification_Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StaircaseSpecification_Width = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StaircaseSpecification_Material = table.Column<int>(type: "int", nullable: true),
                    VentilationSystemSpecification_Count = table.Column<int>(type: "int", nullable: true),
                    LoadBearingWallSpecification_Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoadBearingWallSpecification_Width = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoadBearingWallSpecification_Thickness = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoadBearingWallSpecification_Material = table.Column<int>(type: "int", nullable: true),
                    PartitionWallSpecification_Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartitionWallSpecification_Width = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartitionWallSpecification_Thickness = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartitionWallSpecification_Material = table.Column<int>(type: "int", nullable: true),
                    WindowsSpecification_Amount = table.Column<int>(type: "int", nullable: true),
                    WindowsSpecification_Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WindowsSpecification_Width = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WindowsSpecification_Material = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionSpecifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConstructionSpecifications_ConstructionSpecifications_CellingID",
                        column: x => x.CellingID,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ConstructionSpecifications_ConstructionSpecifications_ChimneyID",
                        column: x => x.ChimneyID,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ConstructionSpecifications_ConstructionSpecifications_FoundationSpecificationID",
                        column: x => x.FoundationSpecificationID,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ConstructionSpecifications_ConstructionSpecifications_LoadBearingWallMaterialID",
                        column: x => x.LoadBearingWallMaterialID,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ConstructionSpecifications_ConstructionSpecifications_PartitionWallID",
                        column: x => x.PartitionWallID,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ConstructionSpecifications_ConstructionSpecifications_RoofID",
                        column: x => x.RoofID,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ConstructionSpecifications_ConstructionSpecifications_VentilationID",
                        column: x => x.VentilationID,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Suppliers_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "MaterialPrices",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialType = table.Column<int>(type: "int", nullable: false),
                    MaterialCategory = table.Column<int>(type: "int", nullable: false),
                    PriceWithoutTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    MaterialPriceType = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    BalconyMaterialPrice_Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PricePerMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SuspendedCeilingMaterialPrice_PricePerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PricePerDoor = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FacadeMaterialPrice_Thickness = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FacadeMaterialPrice_PricePerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PricePerSquareMeterFinish = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FlooringMaterialPrice_PricePerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InsulationOfAtticMaterialPrice_PricePerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Thickness = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PricePerLiter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CoveragePerLiter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PlasteringMaterialPrice_PricePerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CeilingMaterialPrice_PricePerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ChimneyMaterialPrice_PricePerCubicMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PricePerCubicMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoadBearingWallMaterialPrice_PricePerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartitionWallMaterialPrice_PricePerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RoofMaterialPrice_PricePerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PricePerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PricePerStep = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StandardStepHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StandardStepWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PricePerSquareMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StandardHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StandardWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialPrices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaterialPrices_Suppliers_SupplierId",
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
                    RequestedStartTime = table.Column<DateOnly>(type: "date", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ClientProposedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WorkerProposedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AgreedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: true),
                    LastActionBy = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
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
                name: "MaterialOrders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitPriceNet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPriceGross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderAddressId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    MaterialPriceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaterialOrders_MaterialPrices_MaterialPriceId",
                        column: x => x.MaterialPriceId,
                        principalTable: "MaterialPrices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialOrders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialOrders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                    WorkerId = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ConstructionOrderID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionOrderNotifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConstructionOrderNotifications_ConstructionOrders_ConstructionOrderID",
                        column: x => x.ConstructionOrderID,
                        principalTable: "ConstructionOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "OrderAddress",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConstructionOrderId = table.Column<int>(type: "int", nullable: true),
                    MaterialOrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAddress", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderAddress_ConstructionOrders_ConstructionOrderId",
                        column: x => x.ConstructionOrderId,
                        principalTable: "ConstructionOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderAddress_MaterialOrders_MaterialOrderId",
                        column: x => x.MaterialOrderId,
                        principalTable: "MaterialOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_ConstructionSpecifications_CellingID",
                table: "ConstructionSpecifications",
                column: "CellingID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionSpecifications_ChimneyID",
                table: "ConstructionSpecifications",
                column: "ChimneyID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionSpecifications_FoundationSpecificationID",
                table: "ConstructionSpecifications",
                column: "FoundationSpecificationID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionSpecifications_LoadBearingWallMaterialID",
                table: "ConstructionSpecifications",
                column: "LoadBearingWallMaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionSpecifications_PartitionWallID",
                table: "ConstructionSpecifications",
                column: "PartitionWallID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionSpecifications_RoofID",
                table: "ConstructionSpecifications",
                column: "RoofID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionSpecifications_VentilationID",
                table: "ConstructionSpecifications",
                column: "VentilationID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialNotifications_SupplierID",
                table: "MaterialNotifications",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialOrders_MaterialPriceId",
                table: "MaterialOrders",
                column: "MaterialPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialOrders_SupplierId",
                table: "MaterialOrders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialOrders_UserId",
                table: "MaterialOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialPrices_SupplierId",
                table: "MaterialPrices",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAddress_ConstructionOrderId",
                table: "OrderAddress",
                column: "ConstructionOrderId",
                unique: true,
                filter: "[ConstructionOrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAddress_MaterialOrderId",
                table: "OrderAddress",
                column: "MaterialOrderId",
                unique: true,
                filter: "[MaterialOrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_AddressId",
                table: "Suppliers",
                column: "AddressId");

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
                name: "ConstructionOrderNotifications");

            migrationBuilder.DropTable(
                name: "MaterialNotifications");

            migrationBuilder.DropTable(
                name: "OrderAddress");

            migrationBuilder.DropTable(
                name: "ConstructionOrders");

            migrationBuilder.DropTable(
                name: "MaterialOrders");

            migrationBuilder.DropTable(
                name: "ConstructionSpecifications");

            migrationBuilder.DropTable(
                name: "MaterialPrices");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
