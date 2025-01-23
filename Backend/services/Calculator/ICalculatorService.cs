using Backend.Data.Models.Constructions.Specyfication;
using Backend.Data.Models.Price;

namespace Backend.services.Calculator
{
    public interface ICalculatorService
    {
        Task<CalculatedPrice> CalculatePriceAsync(ConstructionSpecification specification);
    }
}
