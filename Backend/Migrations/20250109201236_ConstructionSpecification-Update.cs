using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class ConstructionSpecificationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VentilationSystemCount",
                table: "ConstructionSpecifications",
                newName: "VentilationSystemSpecification_Count");

            migrationBuilder.RenameColumn(
                name: "RoofPitch",
                table: "ConstructionSpecifications",
                newName: "RoofSpecification_Area");

            migrationBuilder.RenameColumn(
                name: "RoofMaterial",
                table: "ConstructionSpecifications",
                newName: "VentilationID");

            migrationBuilder.RenameColumn(
                name: "RoofArea",
                table: "ConstructionSpecifications",
                newName: "Pitch");

            migrationBuilder.RenameColumn(
                name: "PartitionWallWidth",
                table: "ConstructionSpecifications",
                newName: "PartitionWallSpecification_Width");

            migrationBuilder.RenameColumn(
                name: "PartitionWallThickness",
                table: "ConstructionSpecifications",
                newName: "PartitionWallSpecification_Thickness");

            migrationBuilder.RenameColumn(
                name: "PartitionWallMaterial",
                table: "ConstructionSpecifications",
                newName: "RoofSpecification_Material");

            migrationBuilder.RenameColumn(
                name: "PartitionWallHeight",
                table: "ConstructionSpecifications",
                newName: "PartitionWallSpecification_Height");

            migrationBuilder.RenameColumn(
                name: "LoadBearingWallWidth",
                table: "ConstructionSpecifications",
                newName: "LoadBearingWallSpecification_Width");

            migrationBuilder.RenameColumn(
                name: "LoadBearingWallThickness",
                table: "ConstructionSpecifications",
                newName: "LoadBearingWallSpecification_Thickness");

            migrationBuilder.RenameColumn(
                name: "LoadBearingWallMaterial",
                table: "ConstructionSpecifications",
                newName: "RoofID");

            migrationBuilder.RenameColumn(
                name: "LoadBearingWallHeight",
                table: "ConstructionSpecifications",
                newName: "LoadBearingWallSpecification_Height");

            migrationBuilder.RenameColumn(
                name: "ChimneyCount",
                table: "ConstructionSpecifications",
                newName: "PartitionWallSpecification_Material");

            migrationBuilder.RenameColumn(
                name: "CeilingMaterial",
                table: "ConstructionSpecifications",
                newName: "PartitionWallID");

            migrationBuilder.RenameColumn(
                name: "CeilingArea",
                table: "ConstructionSpecifications",
                newName: "FoundationSpecification_Width");

            migrationBuilder.AddColumn<decimal>(
                name: "CelingSpecification_Area",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CelingSpecification_Material",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CellingID",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChimneyID",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Depth",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FoundationSpecification_Length",
                table: "ConstructionSpecifications",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoadBearingWallMaterialID",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoadBearingWallSpecification_Material",
                table: "ConstructionSpecifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionSpecifications_CellingID",
                table: "ConstructionSpecifications",
                column: "CellingID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionSpecifications_ChimneyID",
                table: "ConstructionSpecifications",
                column: "ChimneyID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionSpecifications_LoadBearingWallMaterialID",
                table: "ConstructionSpecifications",
                column: "LoadBearingWallMaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionSpecifications_PartitionWallID",
                table: "ConstructionSpecifications",
                column: "PartitionWallID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionSpecifications_RoofID",
                table: "ConstructionSpecifications",
                column: "RoofID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionSpecifications_VentilationID",
                table: "ConstructionSpecifications",
                column: "VentilationID");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionSpecifications_ConstructionSpecifications_CellingID",
                table: "ConstructionSpecifications",
                column: "CellingID",
                principalTable: "ConstructionSpecifications",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionSpecifications_ConstructionSpecifications_ChimneyID",
                table: "ConstructionSpecifications",
                column: "ChimneyID",
                principalTable: "ConstructionSpecifications",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionSpecifications_ConstructionSpecifications_LoadBearingWallMaterialID",
                table: "ConstructionSpecifications",
                column: "LoadBearingWallMaterialID",
                principalTable: "ConstructionSpecifications",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionSpecifications_ConstructionSpecifications_PartitionWallID",
                table: "ConstructionSpecifications",
                column: "PartitionWallID",
                principalTable: "ConstructionSpecifications",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionSpecifications_ConstructionSpecifications_RoofID",
                table: "ConstructionSpecifications",
                column: "RoofID",
                principalTable: "ConstructionSpecifications",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionSpecifications_ConstructionSpecifications_VentilationID",
                table: "ConstructionSpecifications",
                column: "VentilationID",
                principalTable: "ConstructionSpecifications",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionSpecifications_ConstructionSpecifications_CellingID",
                table: "ConstructionSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionSpecifications_ConstructionSpecifications_ChimneyID",
                table: "ConstructionSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionSpecifications_ConstructionSpecifications_LoadBearingWallMaterialID",
                table: "ConstructionSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionSpecifications_ConstructionSpecifications_PartitionWallID",
                table: "ConstructionSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionSpecifications_ConstructionSpecifications_RoofID",
                table: "ConstructionSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionSpecifications_ConstructionSpecifications_VentilationID",
                table: "ConstructionSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_ConstructionSpecifications_CellingID",
                table: "ConstructionSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_ConstructionSpecifications_ChimneyID",
                table: "ConstructionSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_ConstructionSpecifications_LoadBearingWallMaterialID",
                table: "ConstructionSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_ConstructionSpecifications_PartitionWallID",
                table: "ConstructionSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_ConstructionSpecifications_RoofID",
                table: "ConstructionSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_ConstructionSpecifications_VentilationID",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "CelingSpecification_Area",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "CelingSpecification_Material",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "CellingID",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "ChimneyID",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "Depth",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "FoundationSpecification_Length",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "LoadBearingWallMaterialID",
                table: "ConstructionSpecifications");

            migrationBuilder.DropColumn(
                name: "LoadBearingWallSpecification_Material",
                table: "ConstructionSpecifications");

            migrationBuilder.RenameColumn(
                name: "VentilationSystemSpecification_Count",
                table: "ConstructionSpecifications",
                newName: "VentilationSystemCount");

            migrationBuilder.RenameColumn(
                name: "VentilationID",
                table: "ConstructionSpecifications",
                newName: "RoofMaterial");

            migrationBuilder.RenameColumn(
                name: "RoofSpecification_Material",
                table: "ConstructionSpecifications",
                newName: "PartitionWallMaterial");

            migrationBuilder.RenameColumn(
                name: "RoofSpecification_Area",
                table: "ConstructionSpecifications",
                newName: "RoofPitch");

            migrationBuilder.RenameColumn(
                name: "RoofID",
                table: "ConstructionSpecifications",
                newName: "LoadBearingWallMaterial");

            migrationBuilder.RenameColumn(
                name: "Pitch",
                table: "ConstructionSpecifications",
                newName: "RoofArea");

            migrationBuilder.RenameColumn(
                name: "PartitionWallSpecification_Width",
                table: "ConstructionSpecifications",
                newName: "PartitionWallWidth");

            migrationBuilder.RenameColumn(
                name: "PartitionWallSpecification_Thickness",
                table: "ConstructionSpecifications",
                newName: "PartitionWallThickness");

            migrationBuilder.RenameColumn(
                name: "PartitionWallSpecification_Material",
                table: "ConstructionSpecifications",
                newName: "ChimneyCount");

            migrationBuilder.RenameColumn(
                name: "PartitionWallSpecification_Height",
                table: "ConstructionSpecifications",
                newName: "PartitionWallHeight");

            migrationBuilder.RenameColumn(
                name: "PartitionWallID",
                table: "ConstructionSpecifications",
                newName: "CeilingMaterial");

            migrationBuilder.RenameColumn(
                name: "LoadBearingWallSpecification_Width",
                table: "ConstructionSpecifications",
                newName: "LoadBearingWallWidth");

            migrationBuilder.RenameColumn(
                name: "LoadBearingWallSpecification_Thickness",
                table: "ConstructionSpecifications",
                newName: "LoadBearingWallThickness");

            migrationBuilder.RenameColumn(
                name: "LoadBearingWallSpecification_Height",
                table: "ConstructionSpecifications",
                newName: "LoadBearingWallHeight");

            migrationBuilder.RenameColumn(
                name: "FoundationSpecification_Width",
                table: "ConstructionSpecifications",
                newName: "CeilingArea");
        }
    }
}
