using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class materialordercorrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialOrder_Suppliers_SupplierId",
                table: "MaterialOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialOrder_Users_UserId",
                table: "MaterialOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialPrice_Suppliers_SupplierId",
                table: "MaterialPrice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialPrice",
                table: "MaterialPrice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialOrder",
                table: "MaterialOrder");

            migrationBuilder.RenameTable(
                name: "MaterialPrice",
                newName: "MaterialPrices");

            migrationBuilder.RenameTable(
                name: "MaterialOrder",
                newName: "MaterialOrders");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialPrice_SupplierId",
                table: "MaterialPrices",
                newName: "IX_MaterialPrices_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialOrder_UserId",
                table: "MaterialOrders",
                newName: "IX_MaterialOrders_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialOrder_SupplierId",
                table: "MaterialOrders",
                newName: "IX_MaterialOrders_SupplierId");

            migrationBuilder.AddColumn<int>(
                name: "MaterialOrderID",
                table: "MaterialPrices",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialPrices",
                table: "MaterialPrices",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialOrders",
                table: "MaterialOrders",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialPrices_MaterialOrderID",
                table: "MaterialPrices",
                column: "MaterialOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialOrders_Suppliers_SupplierId",
                table: "MaterialOrders",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialOrders_Users_UserId",
                table: "MaterialOrders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialPrices_MaterialOrders_MaterialOrderID",
                table: "MaterialPrices",
                column: "MaterialOrderID",
                principalTable: "MaterialOrders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialPrices_Suppliers_SupplierId",
                table: "MaterialPrices",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialOrders_Suppliers_SupplierId",
                table: "MaterialOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialOrders_Users_UserId",
                table: "MaterialOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialPrices_MaterialOrders_MaterialOrderID",
                table: "MaterialPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialPrices_Suppliers_SupplierId",
                table: "MaterialPrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialPrices",
                table: "MaterialPrices");

            migrationBuilder.DropIndex(
                name: "IX_MaterialPrices_MaterialOrderID",
                table: "MaterialPrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialOrders",
                table: "MaterialOrders");

            migrationBuilder.DropColumn(
                name: "MaterialOrderID",
                table: "MaterialPrices");

            migrationBuilder.RenameTable(
                name: "MaterialPrices",
                newName: "MaterialPrice");

            migrationBuilder.RenameTable(
                name: "MaterialOrders",
                newName: "MaterialOrder");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialPrices_SupplierId",
                table: "MaterialPrice",
                newName: "IX_MaterialPrice_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialOrders_UserId",
                table: "MaterialOrder",
                newName: "IX_MaterialOrder_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialOrders_SupplierId",
                table: "MaterialOrder",
                newName: "IX_MaterialOrder_SupplierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialPrice",
                table: "MaterialPrice",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialOrder",
                table: "MaterialOrder",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialOrder_Suppliers_SupplierId",
                table: "MaterialOrder",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialOrder_Users_UserId",
                table: "MaterialOrder",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialPrice_Suppliers_SupplierId",
                table: "MaterialPrice",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
