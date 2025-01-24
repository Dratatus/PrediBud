using Backend.Data.Consts;
using Backend.Data.Models.Constructions.Specyfication.Walls;
using Backend.Middlewares;

namespace Backend.Validatiors.Calculator
{
    public static class LoadBearingWallSpecificationValidator
    {
        public static void Validate(LoadBearingWallSpecification spec)
        {
            if (spec == null)
            {
                throw new ApiException(ErrorMessages.SpecificationCannotBeNull, StatusCodes.Status400BadRequest);
            }

            if (spec.Height <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidWallHeight, StatusCodes.Status400BadRequest);
            }

            if (spec.Width <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidWallWidth, StatusCodes.Status400BadRequest);
            }

            if (spec.Thickness <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidWallThickness, StatusCodes.Status400BadRequest);
            }
        }
    }

}
