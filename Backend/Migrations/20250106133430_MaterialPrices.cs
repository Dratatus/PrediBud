using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class MaterialPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PriceWithoutTax",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerUnit",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "MaterialType",
                table: "MaterialPrices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaterialCategory",
                table: "MaterialPrices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BalconyMaterialPrice_Height",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CeilingMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ChimneyMaterialPrice_PricePerUnit",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CoveragePerLiter",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FacadeMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FacadeMaterialPrice_Thickness",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FlooringMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "InsulationOfAtticMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LoadBearingWallMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialPriceType",
                table: "MaterialPrices",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "MaxHeight",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PartitionWallMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PlasteringMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerCubicMeter",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerDoor",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerLiter",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerMeter",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerSquareMeter",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerSquareMeterFinish",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SuspendedCeilingMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Thickness",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalconyMaterialPrice_Height",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "CeilingMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "ChimneyMaterialPrice_PricePerUnit",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "CoveragePerLiter",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "FacadeMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "FacadeMaterialPrice_Thickness",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "FlooringMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "InsulationOfAtticMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "LoadBearingWallMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "MaterialPriceType",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "MaxHeight",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "PartitionWallMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "PlasteringMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "PricePerCubicMeter",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "PricePerDoor",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "PricePerLiter",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "PricePerMeter",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "PricePerSquareMeter",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "PricePerSquareMeterFinish",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "SuspendedCeilingMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "Thickness",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "MaterialPrices");

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceWithoutTax",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerUnit",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaterialType",
                table: "MaterialPrices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "MaterialCategory",
                table: "MaterialPrices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
