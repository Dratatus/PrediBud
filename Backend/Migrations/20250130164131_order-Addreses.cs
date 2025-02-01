using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class orderAddreses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "ConstructionOrderNotifications");

            migrationBuilder.AddColumn<int>(
                name: "OrderAddressId",
                table: "MaterialOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "ConstructionOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderAddress");

            migrationBuilder.DropColumn(
                name: "OrderAddressId",
                table: "MaterialOrders");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "ConstructionOrders");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "ConstructionOrderNotifications",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
