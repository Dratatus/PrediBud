using Backend.Data.Consts;
using Backend.Data.Models.Constructions.Specyfication.Foundation;
using Backend.Middlewares;

namespace Backend.Validatiors.Calculator
{
    public static class FoundationSpecificationValidator
    {
        public static void Validate(FoundationSpecification spec)
        {
            if (spec == null)
            {
                throw new ApiException(ErrorMessages.SpecificationCannotBeNull, StatusCodes.Status400BadRequest);
            }

            if (spec.Length <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidFoundationLength, StatusCodes.Status400BadRequest);
            }

            if (spec.Width <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidFoundationWidth, StatusCodes.Status400BadRequest);
            }

            if (spec.Depth <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidFoundationDepth, StatusCodes.Status400BadRequest);
            }
        }
    }
}
