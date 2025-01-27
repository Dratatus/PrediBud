namespace Backend.Data.Models.Constructions.Specyfication.Chimney
{
    public class ChimneySpecification: ConstructionSpecification
    {
        public ChimneySpecification()
        {
            Type = ConstructionType.Chimney;
        }

        public int Count { get; set; }  
    }
}
