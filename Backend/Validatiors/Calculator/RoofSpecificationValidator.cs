using Backend.Data.Consts;
using Backend.Data.Models.Constructions.Specyfication.Roof;
using Backend.Middlewares;

namespace Backend.Validatiors.Calculator
{
    public static class RoofSpecificationValidator
    {
        public static void Validate(RoofSpecification spec)
        {
            if (spec == null)
            {
                throw new ApiException(ErrorMessages.SpecificationCannotBeNull, StatusCodes.Status400BadRequest);
            }

            if (spec.Area <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidRoofArea, StatusCodes.Status400BadRequest);
            }

            if (spec.Pitch < 0 || spec.Pitch > 90)
            {
                throw new ApiException(ErrorMessages.InvalidRoofPitch, StatusCodes.Status400BadRequest);
            }
        }
    }

}
