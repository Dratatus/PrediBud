using Backend.Data.Models.Constructions.Specyfication.Ceiling;
using Backend.Data.Models.Constructions.Specyfication.Roof;
using Backend.Data.Models.Constructions.Specyfication.Walls;

namespace Backend.DTO.Specyfication
{
    public class ShellOpenSpecificationDetails
    {
        public decimal FoundationLength { get; set; }
        public decimal FoundationWidth { get; set; }
        public decimal FoundationDepth { get; set; }

        public decimal LoadBearingWallHeight { get; set; }
        public decimal LoadBearingWallWidth { get; set; }
        public decimal LoadBearingWallThickness { get; set; }
        public LoadBearingWallMaterial? LoadBearingWallMaterial { get; set; }

        public decimal PartitionWallHeight { get; set; }
        public decimal PartitionWallWidth { get; set; }
        public decimal PartitionWallThickness { get; set; }
        public PartitionWallMaterial? PartitionWallMaterial { get; set; }

        public int ChimneyCount { get; set; }
        public int VentilationSystemCount { get; set; }

        public decimal CeilingArea { get; set; }
        public CeilingMaterial? CeilingMaterial { get; set; }

        public decimal RoofArea { get; set; }
        public RoofMaterial? RoofMaterial { get; set; }
        public decimal RoofPitch { get; set; }

        public string[] ImagesUrl { get; set; }
    }
}
