using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class notificationfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "ConstructionOrderNotifications");

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "ConstructionOrderNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "ConstructionOrderNotifications");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "ConstructionOrderNotifications",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
