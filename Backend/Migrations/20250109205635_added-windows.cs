using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class addedwindows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DoorsSpecification_Width",
                table: "ConstructionSpecifications",
                newName: "WindowsSpecification_Width");

            migrationBuilder.RenameColumn(
                name: "DoorsSpecification_Amount",
                table: "ConstructionSpecifications",
                newName: "WindowsSpecification_Amount");

            migrationBuilder.AddColumn<decimal>(
                name: "RoofMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "StandardHeight",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "StandardWidth",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WindowsSpecification_Material",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoofMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "StandardHeight",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "StandardWidth",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "WindowsSpecification_Material",
                table: "ConstructionSpecifications");

            migrationBuilder.RenameColumn(
                name: "WindowsSpecification_Width",
                table: "ConstructionSpecifications",
                newName: "DoorsSpecification_Width");

            migrationBuilder.RenameColumn(
                name: "WindowsSpecification_Amount",
                table: "ConstructionSpecifications",
                newName: "DoorsSpecification_Amount");
        }
    }
}
