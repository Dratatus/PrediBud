using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class ChimneyUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChimneyMaterialPrice_PricePerUnit",
                table: "MaterialPrices",
                newName: "ChimneyMaterialPrice_PricePerCubicMeter");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChimneyMaterialPrice_PricePerCubicMeter",
                table: "MaterialPrices",
                newName: "ChimneyMaterialPrice_PricePerUnit");
        }
    }
}
