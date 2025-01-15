namespace Backend.Data.Models.Constructions.Specyfication.Roof
{
    public class RoofSpecification: ConstructionSpecification
    {
        public RoofSpecification()
        {
            Type = ConstructionType.Roof;
        }
        public decimal Area { get; set; }
        public RoofMaterial Material { get; set; }  
        public decimal Pitch { get; set; }
    }
}
