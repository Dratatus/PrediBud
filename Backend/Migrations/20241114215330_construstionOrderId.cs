using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class construstionOrderId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionOrderNotifications_ConstructionOrders_ConstructionOrderID",
                table: "ConstructionOrderNotifications");

            migrationBuilder.AlterColumn<int>(
                name: "ConstructionOrderID",
                table: "ConstructionOrderNotifications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionOrderNotifications_ConstructionOrders_ConstructionOrderID",
                table: "ConstructionOrderNotifications",
                column: "ConstructionOrderID",
                principalTable: "ConstructionOrders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionOrderNotifications_ConstructionOrders_ConstructionOrderID",
                table: "ConstructionOrderNotifications");

            migrationBuilder.AlterColumn<int>(
                name: "ConstructionOrderID",
                table: "ConstructionOrderNotifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionOrderNotifications_ConstructionOrders_ConstructionOrderID",
                table: "ConstructionOrderNotifications",
                column: "ConstructionOrderID",
                principalTable: "ConstructionOrders",
                principalColumn: "ID");
        }
    }
}
