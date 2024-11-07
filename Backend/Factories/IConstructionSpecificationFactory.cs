using Backend.Data.Models.Constructions.Specyfication;
using Backend.Data.Models.Constructions;

namespace Backend.Factories
{
    public interface IConstructionSpecificationFactory
    {
        ConstructionSpecification CreateSpecification(ConstructionType type, object details);
    }
}
