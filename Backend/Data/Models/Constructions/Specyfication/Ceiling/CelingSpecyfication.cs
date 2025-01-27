namespace Backend.Data.Models.Constructions.Specyfication.Ceiling
{
    public class CeilingSpecification: ConstructionSpecification
    {
        public CeilingSpecification()
        {
            Type = ConstructionType.Ceiling;
        }
        public decimal Area { get; set; }  
        public CeilingMaterial Material { get; set; }  
    }
}
