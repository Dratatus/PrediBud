using Backend.Data.Consts;
using Backend.Data.Models.Constructions.Dimensions.Floor;
using Backend.Middlewares;

namespace Backend.Validatiors.Calculator
{
    public static class FlooringSpecificationValidator
    {
        public static void Validate(FlooringSpecification spec)
        {
            if (spec == null)
            {
                throw new ApiException(ErrorMessages.SpecificationCannotBeNull, StatusCodes.Status400BadRequest);
            }

            if (spec.Area <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidFlooringArea, StatusCodes.Status400BadRequest);
            }
        }
    }
}
