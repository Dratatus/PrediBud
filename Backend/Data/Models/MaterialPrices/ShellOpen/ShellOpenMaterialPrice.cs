using Backend.Data.Models.Constructions.Specyfication.ShellOpen;
using Backend.Data.Models.Suppliers;

namespace Backend.Data.Models.MaterialPrices.ShellOpen
{
    public class ShellOpenMaterialPrice : MaterialPrice
    {
        public decimal FoundationPricePerCubicMeter { get; set; }

        public decimal LoadBearingWallPricePerSquareMeter { get; set; }

        public decimal PartitionWallPricePerSquareMeter { get; set; }

        public decimal ChimneyPricePerUnit { get; set; }

        public decimal VentilationSystemPricePerUnit { get; set; }

        public decimal CeilingPricePerSquareMeter { get; set; }

        public decimal RoofPricePerSquareMeter { get; set; }
    }
}
