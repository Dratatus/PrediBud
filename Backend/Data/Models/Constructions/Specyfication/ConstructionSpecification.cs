using Backend.Conventer;
using Backend.Data.Models.Constructions.Dimensions;
using System.Text.Json.Serialization;

namespace Backend.Data.Models.Constructions.Specyfication
{
    public abstract class ConstructionSpecification : IConstructionSpecification
    {
        public int ID { get; set; }  
        public ConstructionType Type { get; set; } 
    }
}
