using Backend.Data.Models.Constructions.Specyfication.ShellOpen;

namespace Backend.DTO.Specyfication
{
    public class ShellOpenSpecificationDetails
    {
        // Właściwości dla fundamentów
        public decimal? FoundationLength { get; set; }
        public decimal? FoundationWidth { get; set; }
        public decimal? FoundationDepth { get; set; }

        // Właściwości dla ścian nośnych
        public decimal? LoadBearingWallHeight { get; set; }
        public decimal? LoadBearingWallWidth { get; set; }
        public decimal? LoadBearingWallThickness { get; set; }
        public LoadBearingWallMaterial LoadBearingWallMaterial { get; set; }

        // Właściwości dla ścian działowych
        public decimal? PartitionWallHeight { get; set; }
        public decimal? PartitionWallWidth { get; set; }
        public decimal? PartitionWallThickness { get; set; }
        public PartitionWallMaterial PartitionWallMaterial { get; set; }

        // Właściwości dla systemów kominowych i wentylacyjnych
        public int? ChimneyCount { get; set; }
        public int? VentilationSystemCount { get; set; }

        // Właściwości dla stropu
        public decimal? CeilingArea { get; set; }
        public CeilingMaterial CeilingMaterial { get; set; }

        // Właściwości dla dachu
        public decimal? RoofArea { get; set; }
        public RoofMaterial RoofMaterial { get; set; }
        public decimal? RoofPitch { get; set; }

        // Dodatkowe właściwości
        public string[] ImagesUrl { get; set; }
    }
}
