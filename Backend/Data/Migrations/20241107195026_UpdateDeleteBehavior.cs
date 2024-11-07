using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "ConstructionOrders",
                newName: "placementPhotos");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ConstructionOrders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ConstructionOrders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<decimal>(
                name: "AgreedPrice",
                table: "ConstructionOrders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ClientProposedPrice",
                table: "ConstructionOrders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConstructionSpecificationId",
                table: "ConstructionOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ConstructionType",
                table: "ConstructionOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ConstructionOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestedStartTime",
                table: "ConstructionOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WorkerProposedPrice",
                table: "ConstructionOrders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionSpecifications", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BalconySpecifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RailingMaterial = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalconySpecifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BalconySpecifications_ConstructionSpecifications_ID",
                        column: x => x.ID,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoorsSpecifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Material = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorsSpecifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DoorsSpecifications_ConstructionSpecifications_ID",
                        column: x => x.ID,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacadeSpecifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    SurfaceArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsulationType = table.Column<int>(type: "int", nullable: false),
                    FinishMaterial = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacadeSpecifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FacadeSpecifications_ConstructionSpecifications_ID",
                        column: x => x.ID,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlooringSpecifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlooringSpecifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FlooringSpecifications_ConstructionSpecifications_ID",
                        column: x => x.ID,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsulationOfAtticSpecifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Material = table.Column<int>(type: "int", nullable: false),
                    Thickness = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsulationOfAtticSpecifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InsulationOfAtticSpecifications_ConstructionSpecifications_ID",
                        column: x => x.ID,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaintingSpecifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    WallSurfaceArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaintType = table.Column<int>(type: "int", nullable: false),
                    NumberOfCoats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaintingSpecifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaintingSpecifications_ConstructionSpecifications_ID",
                        column: x => x.ID,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlasteringSpecifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    WallSurfaceArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlasterType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlasteringSpecifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlasteringSpecifications_ConstructionSpecifications_ID",
                        column: x => x.ID,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShellOpenSpecifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    FoundationLength = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FoundationWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FoundationDepth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoadBearingWallHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoadBearingWallWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoadBearingWallThickness = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoadBearingWallMaterial = table.Column<int>(type: "int", nullable: false),
                    PartitionWallHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartitionWallWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartitionWallThickness = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartitionWallMaterial = table.Column<int>(type: "int", nullable: false),
                    ChimneyCount = table.Column<int>(type: "int", nullable: true),
                    VentilationSystemCount = table.Column<int>(type: "int", nullable: true),
                    CeilingArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CeilingMaterial = table.Column<int>(type: "int", nullable: false),
                    RoofArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RoofMaterial = table.Column<int>(type: "int", nullable: false),
                    RoofPitch = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ImagesUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShellOpenSpecifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShellOpenSpecifications_ConstructionSpecifications_ID",
                        column: x => x.ID,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaircaseSpecifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NumberOfSteps = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Material = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaircaseSpecifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StaircaseSpecifications_ConstructionSpecifications_ID",
                        column: x => x.ID,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuspendedCeilingSpecifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Material = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuspendedCeilingSpecifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SuspendedCeilingSpecifications_ConstructionSpecifications_ID",
                        column: x => x.ID,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WindowsSpecifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WindowsSpecifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WindowsSpecifications_ConstructionSpecifications_ID",
                        column: x => x.ID,
                        principalTable: "ConstructionSpecifications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionOrders_ConstructionSpecificationId",
                table: "ConstructionOrders",
                column: "ConstructionSpecificationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionOrders_ConstructionSpecifications_ConstructionSpecificationId",
                table: "ConstructionOrders",
                column: "ConstructionSpecificationId",
                principalTable: "ConstructionSpecifications",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_AddressId",
                table: "Users",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionOrders_ConstructionSpecifications_ConstructionSpecificationId",
                table: "ConstructionOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_AddressId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "BalconySpecifications");

            migrationBuilder.DropTable(
                name: "DoorsSpecifications");

            migrationBuilder.DropTable(
                name: "FacadeSpecifications");

            migrationBuilder.DropTable(
                name: "FlooringSpecifications");

            migrationBuilder.DropTable(
                name: "InsulationOfAtticSpecifications");

            migrationBuilder.DropTable(
                name: "PaintingSpecifications");

            migrationBuilder.DropTable(
                name: "PlasteringSpecifications");

            migrationBuilder.DropTable(
                name: "ShellOpenSpecifications");

            migrationBuilder.DropTable(
                name: "StaircaseSpecifications");

            migrationBuilder.DropTable(
                name: "SuspendedCeilingSpecifications");

            migrationBuilder.DropTable(
                name: "WindowsSpecifications");

            migrationBuilder.DropTable(
                name: "ConstructionSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_Users_AddressId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ConstructionOrders_ConstructionSpecificationId",
                table: "ConstructionOrders");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AgreedPrice",
                table: "ConstructionOrders");

            migrationBuilder.DropColumn(
                name: "ClientProposedPrice",
                table: "ConstructionOrders");

            migrationBuilder.DropColumn(
                name: "ConstructionSpecificationId",
                table: "ConstructionOrders");

            migrationBuilder.DropColumn(
                name: "ConstructionType",
                table: "ConstructionOrders");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ConstructionOrders");

            migrationBuilder.DropColumn(
                name: "RequestedStartTime",
                table: "ConstructionOrders");

            migrationBuilder.DropColumn(
                name: "WorkerProposedPrice",
                table: "ConstructionOrders");

            migrationBuilder.RenameColumn(
                name: "placementPhotos",
                table: "ConstructionOrders",
                newName: "Address");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ConstructionOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ConstructionOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
