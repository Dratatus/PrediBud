using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class materialPriceupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialPrices_MaterialOrders_MaterialOrderID",
                table: "MaterialPrices");

            migrationBuilder.DropIndex(
                name: "IX_MaterialPrices_MaterialOrderID",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "MaterialOrderID",
                table: "MaterialPrices");

            migrationBuilder.DropColumn(
                name: "MaterialCategory",
                table: "MaterialOrders");

            migrationBuilder.DropColumn(
                name: "MaterialType",
                table: "MaterialOrders");

            migrationBuilder.DropColumn(
                name: "Taxes",
                table: "MaterialOrders");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "MaterialOrders",
                newName: "UnitPriceNet");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "MaterialOrders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MaterialPriceId",
                table: "MaterialOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPriceGross",
                table: "MaterialOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialOrders_MaterialPriceId",
                table: "MaterialOrders",
                column: "MaterialPriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialOrders_MaterialPrices_MaterialPriceId",
                table: "MaterialOrders",
                column: "MaterialPriceId",
                principalTable: "MaterialPrices",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialOrders_MaterialPrices_MaterialPriceId",
                table: "MaterialOrders");

            migrationBuilder.DropIndex(
                name: "IX_MaterialOrders_MaterialPriceId",
                table: "MaterialOrders");

            migrationBuilder.DropColumn(
                name: "MaterialPriceId",
                table: "MaterialOrders");

            migrationBuilder.DropColumn(
                name: "UnitPriceGross",
                table: "MaterialOrders");

            migrationBuilder.RenameColumn(
                name: "UnitPriceNet",
                table: "MaterialOrders",
                newName: "UnitPrice");

            migrationBuilder.AddColumn<int>(
                name: "MaterialOrderID",
                table: "MaterialPrices",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "MaterialOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "MaterialCategory",
                table: "MaterialOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialType",
                table: "MaterialOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Taxes",
                table: "MaterialOrders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialPrices_MaterialOrderID",
                table: "MaterialPrices",
                column: "MaterialOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialPrices_MaterialOrders_MaterialOrderID",
                table: "MaterialPrices",
                column: "MaterialOrderID",
                principalTable: "MaterialOrders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
