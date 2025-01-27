using Backend.Data.Consts;
using Backend.Data.Models.Constructions.Specyfication.Ventilation;
using Backend.Middlewares;

namespace Backend.Validatiors.Calculator
{
    public static class VentilationSystemSpecificationValidator
    {
        public static void Validate(VentilationSystemSpecification spec)
        {
            if (spec == null)
            {
                throw new ApiException(ErrorMessages.SpecificationCannotBeNull, StatusCodes.Status400BadRequest);
            }

            if (spec.Count <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidVentilationSystemCount, StatusCodes.Status400BadRequest);
            }
        }
    }

}
