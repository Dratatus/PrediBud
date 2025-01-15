namespace Backend.Data.Models.Constructions.Specyfication.Ventilation
{
    public class VentilationSystemSpecification: ConstructionSpecification
    {
        public VentilationSystemSpecification()
        {
            Type = ConstructionType.VentilationSystem;
        }
        public int Count { get; set; } 
    }
}
