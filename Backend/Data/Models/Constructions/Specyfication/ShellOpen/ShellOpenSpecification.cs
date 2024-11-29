using Backend.Data.Models.Constructions.Dimensions;

namespace Backend.Data.Models.Constructions.Specyfication.ShellOpen
{
    public class ShellOpenSpecification: ConstructionSpecification
    {
        public ShellOpenSpecification()
        {
            Type = ConstructionType.ShellOpen;
        }

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
        public int? ChimneyCount { get; set; }  // Liczba kominów
        public int? VentilationSystemCount { get; set; }  // Liczba systemów wentylacyjnych

        // Właściwości dla stropu
        public decimal? CeilingArea { get; set; }  // Powierzchnia stropu
        public CeilingMaterial CeilingMaterial { get; set; }  // Materiał stropu

        // Właściwości dla dachu
        public decimal? RoofArea { get; set; }  // Powierzchnia dachu
        public RoofMaterial RoofMaterial { get; set; }  // Materiał dachu
        public decimal? RoofPitch { get; set; }  // Nachylenie dachu (w stopniach)

        // Dodatkowe właściwości
        public string[] ImagesUrl { get; set; }  // Zdjęcia konstrukcji/planu
    }
}
