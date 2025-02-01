using Backend.Conventer;
using Backend.Data.Models.Constructions.Dimensions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Data.Models.Constructions.Specyfication
{
    public abstract class ConstructionSpecification : IConstructionSpecification
    {
        public int ID { get; set; }  
        public ConstructionType Type { get; set; }

        [NotMapped]
        public decimal? ClientProvidedPrice { get; set; }

        [NotMapped]
        public bool? IsPriceGross { get; set; } 
    }
}
