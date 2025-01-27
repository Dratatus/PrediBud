using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Ceilingupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoundationDepth",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "FoundationLength",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "FoundationWidth",
                table: "ConstructionSpecifications");

            migrationBuilder.RenameColumn(
                name: "CelingSpecification_Material",
                table: "ConstructionSpecifications",
                newName: "CeilingSpecification_Material");

            migrationBuilder.RenameColumn(
                name: "CelingSpecification_Area",
                table: "ConstructionSpecifications",
                newName: "CeilingSpecification_Area");

            migrationBuilder.AddColumn<int>(
                name: "FoundationSpecificationID",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionSpecifications_FoundationSpecificationID",
                table: "ConstructionSpecifications",
                column: "FoundationSpecificationID");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionSpecifications_ConstructionSpecifications_FoundationSpecificationID",
                table: "ConstructionSpecifications",
                column: "FoundationSpecificationID",
                principalTable: "ConstructionSpecifications",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionSpecifications_ConstructionSpecifications_FoundationSpecificationID",
                table: "ConstructionSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_ConstructionSpecifications_FoundationSpecificationID",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "FoundationSpecificationID",
                table: "ConstructionSpecifications");

            migrationBuilder.RenameColumn(
                name: "CeilingSpecification_Material",
                table: "ConstructionSpecifications",
                newName: "CelingSpecification_Material");

            migrationBuilder.RenameColumn(
                name: "CeilingSpecification_Area",
                table: "ConstructionSpecifications",
                newName: "CelingSpecification_Area");

            migrationBuilder.AddColumn<decimal>(
                name: "FoundationDepth",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FoundationLength",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FoundationWidth",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
