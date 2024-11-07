namespace Backend.Data.Models.Constructions.Specyfication.Plastering
{
    public class PlasteringSpecification: ConstructionSpecification
    {
        public PlasteringSpecification()
        {
            Type = ConstructionType.Plastering;
        }
        public decimal WallSurfaceArea { get; set; }  // Powierzchnia ścian do otynkowania
        public PlasterType PlasterType { get; set; }  // Typ tynku
    }
}
