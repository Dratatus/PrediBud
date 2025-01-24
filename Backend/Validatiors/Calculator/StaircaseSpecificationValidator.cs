using Backend.Data.Consts;
using Backend.Data.Models.Constructions.Specyfication.Stairs;
using Backend.Middlewares;

namespace Backend.Validatiors.Calculator
{
    public static class StaircaseSpecificationValidator
    {
        public static void Validate(StaircaseSpecification spec)
        {
            if (spec == null)
            {
                throw new ApiException(ErrorMessages.SpecificationCannotBeNull, StatusCodes.Status400BadRequest);
            }

            if (spec.NumberOfSteps <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidNumberOfSteps, StatusCodes.Status400BadRequest);
            }

            if (spec.Height <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidStaircaseHeight, StatusCodes.Status400BadRequest);
            }

            if (spec.Width <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidStaircaseWidth, StatusCodes.Status400BadRequest);
            }
        }
    }

}
