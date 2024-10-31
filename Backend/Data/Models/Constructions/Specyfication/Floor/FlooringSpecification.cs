namespace Backend.Data.Models.Constructions.Dimensions.Floor
{
    public class FlooringSpecification : IConstructionDimensions
    {
        public ConstructionType Type => ConstructionType.Flooring;
        public decimal Area { get; set; }
        public string Material { get; set; }  // Typ podłogi (np. panele, parkiet, kafelki)
    }
}
