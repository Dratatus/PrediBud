using Backend.Data.Consts;
using Backend.Data.Models.Constructions.Dimensions.Doors;
using Backend.Middlewares;

namespace Backend.Validatiors.Calculator
{
    public static class DoorsSpecificationValidator
    {
        public static void Validate(DoorsSpecification spec)
        {
            if (spec == null)
            {
                throw new ApiException(ErrorMessages.SpecificationCannotBeNull, StatusCodes.Status400BadRequest);
            }

            if (spec.Amount <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidDoorsAmount, StatusCodes.Status400BadRequest);
            }

            if (spec.Height <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidDoorsHeight, StatusCodes.Status400BadRequest);
            }

            if (spec.Width <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidDoorsWidth, StatusCodes.Status400BadRequest);
            }
        }
    }
}
