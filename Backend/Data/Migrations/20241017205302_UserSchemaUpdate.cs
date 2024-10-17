using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserSchemaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionOrderNotifications_Workers_WorkerId",
                table: "ConstructionOrderNotifications");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.AddColumn<int>(
                name: "ClientID",
                table: "MaterialOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConstructionOrderID",
                table: "ConstructionOrderNotifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserType = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ContactDetails_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactDetails_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Credentials_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Credentials_PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ConstructionOrders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConstructionOrders_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConstructionOrders_Users_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialOrders_ClientID",
                table: "MaterialOrders",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionOrderNotifications_ConstructionOrderID",
                table: "ConstructionOrderNotifications",
                column: "ConstructionOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionOrders_ClientId",
                table: "ConstructionOrders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionOrders_WorkerId",
                table: "ConstructionOrders",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionOrderNotifications_ConstructionOrders_ConstructionOrderID",
                table: "ConstructionOrderNotifications",
                column: "ConstructionOrderID",
                principalTable: "ConstructionOrders",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionOrderNotifications_Users_WorkerId",
                table: "ConstructionOrderNotifications",
                column: "WorkerId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialOrders_Users_ClientID",
                table: "MaterialOrders",
                column: "ClientID",
                principalTable: "Users",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionOrderNotifications_ConstructionOrders_ConstructionOrderID",
                table: "ConstructionOrderNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionOrderNotifications_Users_WorkerId",
                table: "ConstructionOrderNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialOrders_Users_ClientID",
                table: "MaterialOrders");

            migrationBuilder.DropTable(
                name: "ConstructionOrders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_MaterialOrders_ClientID",
                table: "MaterialOrders");

            migrationBuilder.DropIndex(
                name: "IX_ConstructionOrderNotifications_ConstructionOrderID",
                table: "ConstructionOrderNotifications");

            migrationBuilder.DropColumn(
                name: "ClientID",
                table: "MaterialOrders");

            migrationBuilder.DropColumn(
                name: "ConstructionOrderID",
                table: "ConstructionOrderNotifications");

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionOrderNotifications_Workers_WorkerId",
                table: "ConstructionOrderNotifications",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
