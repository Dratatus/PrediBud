using Backend.Data.Consts;
using Backend.Data.Models.Constructions.Specyfication.Walls;
using Backend.Middlewares;

namespace Backend.Validatiors.Calculator
{
    public static class PartitionWallSpecificationValidator
    {
        public static void Validate(PartitionWallSpecification spec)
        {
            if (spec == null)
            {
                throw new ApiException(ErrorMessages.SpecificationCannotBeNull, StatusCodes.Status400BadRequest);
            }

            if (!spec.Height.HasValue || spec.Height <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidWallHeight, StatusCodes.Status400BadRequest);
            }

            if (!spec.Width.HasValue || spec.Width <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidWallWidth, StatusCodes.Status400BadRequest);
            }

            if (!spec.Thickness.HasValue || spec.Thickness <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidWallThickness, StatusCodes.Status400BadRequest);
            }
        }
    }

}
