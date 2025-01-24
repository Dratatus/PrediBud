using Backend.Data.Consts;
using Backend.Data.Models.Constructions.Specyfication.Ceiling;
using Backend.Middlewares;

namespace Backend.Validatiors.Calculator
{
    public static class CeilingSpecificationValidator
    {
        public static void Validate(CeilingSpecification spec)
        {
            if (spec == null)
            {
                throw new ApiException(ErrorMessages.SpecificationCannotBeNull, StatusCodes.Status400BadRequest);
            }

            if (spec.Area <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidCeilingArea, StatusCodes.Status400BadRequest);
            }
        }
    }
}
