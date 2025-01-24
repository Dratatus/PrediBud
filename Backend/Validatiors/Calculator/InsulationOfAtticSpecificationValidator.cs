using Backend.Data.Consts;
using Backend.Data.Models.Constructions.Dimensions;
using Backend.Middlewares;

namespace Backend.Validatiors.Calculator
{
    public static class InsulationOfAtticSpecificationValidator
    {
        public static void Validate(InsulationOfAtticSpecification spec)
        {
            if (spec == null)
            {
                throw new ApiException(ErrorMessages.SpecificationCannotBeNull, StatusCodes.Status400BadRequest);
            }

            if (spec.Area <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidInsulationArea, StatusCodes.Status400BadRequest);
            }

            if (spec.Thickness <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidInsulationThickness, StatusCodes.Status400BadRequest);
            }
        }
    }
}
