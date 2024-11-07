using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscriminatorToConstructionSpecifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Area",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BalconySpecification_Width",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CeilingArea",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CeilingMaterial",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChimneyCount",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoorsSpecification_Amount",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DoorsSpecification_Height",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoorsSpecification_Material",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DoorsSpecification_Width",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinishMaterial",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FlooringSpecification_Area",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FlooringSpecification_Material",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FoundationDepth",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FoundationLength",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FoundationWidth",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagesUrl",
                table: "ConstructionSpecifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "InsulationOfAtticSpecification_Area",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InsulationOfAtticSpecification_Material",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InsulationType",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Length",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LoadBearingWallHeight",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoadBearingWallMaterial",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LoadBearingWallThickness",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LoadBearingWallWidth",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Material",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfCoats",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSteps",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaintType",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PartitionWallHeight",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartitionWallMaterial",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PartitionWallThickness",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PartitionWallWidth",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlasterType",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PlasteringSpecification_WallSurfaceArea",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RailingMaterial",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RoofArea",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoofMaterial",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RoofPitch",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "StaircaseSpecification_Height",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaircaseSpecification_Material",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "StaircaseSpecification_Width",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SurfaceArea",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Thickness",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VentilationSystemCount",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WallSurfaceArea",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WindowsSpecification_Height",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "BalconySpecification_Width",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "CeilingArea",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "CeilingMaterial",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "ChimneyCount",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "DoorsSpecification_Amount",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "DoorsSpecification_Height",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "DoorsSpecification_Material",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "DoorsSpecification_Width",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "FinishMaterial",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "FlooringSpecification_Area",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "FlooringSpecification_Material",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "FoundationDepth",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "FoundationLength",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "FoundationWidth",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "ImagesUrl",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "InsulationOfAtticSpecification_Area",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "InsulationOfAtticSpecification_Material",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "InsulationType",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "LoadBearingWallHeight",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "LoadBearingWallMaterial",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "LoadBearingWallThickness",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "LoadBearingWallWidth",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "NumberOfCoats",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "NumberOfSteps",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "PaintType",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "PartitionWallHeight",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "PartitionWallMaterial",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "PartitionWallThickness",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "PartitionWallWidth",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "PlasterType",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "PlasteringSpecification_WallSurfaceArea",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "RailingMaterial",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "RoofArea",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "RoofMaterial",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "RoofPitch",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "StaircaseSpecification_Height",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "StaircaseSpecification_Material",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "StaircaseSpecification_Width",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "SurfaceArea",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "Thickness",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "VentilationSystemCount",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "WallSurfaceArea",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "WindowsSpecification_Height",
                table: "ConstructionSpecifications");

            migrationBuilder.CreateTable(
                name: "BalconySpecifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RailingMaterial = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                    Material = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                    FinishMaterial = table.Column<int>(type: "int", nullable: false),
                    InsulationType = table.Column<int>(type: "int", nullable: false),
                    SurfaceArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                    NumberOfCoats = table.Column<int>(type: "int", nullable: false),
                    PaintType = table.Column<int>(type: "int", nullable: false),
                    WallSurfaceArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                    PlasterType = table.Column<int>(type: "int", nullable: false),
                    WallSurfaceArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                    CeilingArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CeilingMaterial = table.Column<int>(type: "int", nullable: false),
                    ChimneyCount = table.Column<int>(type: "int", nullable: true),
                    FoundationDepth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FoundationLength = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FoundationWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ImagesUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoadBearingWallHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoadBearingWallMaterial = table.Column<int>(type: "int", nullable: false),
                    LoadBearingWallThickness = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoadBearingWallWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartitionWallHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartitionWallMaterial = table.Column<int>(type: "int", nullable: false),
                    PartitionWallThickness = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PartitionWallWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RoofArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RoofMaterial = table.Column<int>(type: "int", nullable: false),
                    RoofPitch = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VentilationSystemCount = table.Column<int>(type: "int", nullable: true)
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
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Material = table.Column<int>(type: "int", nullable: false),
                    NumberOfSteps = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
        }
    }
}
