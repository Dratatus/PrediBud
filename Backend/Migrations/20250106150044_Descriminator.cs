using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Descriminator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CeilingMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices");

            migrationBuilder.AlterColumn<string>(
                name: "MaterialPriceType",
                table: "MaterialPrices",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(34)",
                oldMaxLength: 34);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MaterialPriceType",
                table: "MaterialPrices",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(21)",
                oldMaxLength: 21);

            migrationBuilder.AddColumn<decimal>(
                name: "CeilingMaterialPrice_PricePerSquareMeter",
                table: "MaterialPrices",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
