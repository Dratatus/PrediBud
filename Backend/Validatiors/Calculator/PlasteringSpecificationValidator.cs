using Backend.Data.Consts;
using Backend.Data.Models.Constructions.Specyfication.Plastering;
using Backend.Middlewares;

namespace Backend.Validatiors.Calculator
{
    public static class PlasteringSpecificationValidator
    {
        public static void Validate(PlasteringSpecification spec)
        {
            if (spec == null)
            {
                throw new ApiException(ErrorMessages.SpecificationCannotBeNull, StatusCodes.Status400BadRequest);
            }

            if (spec.WallSurfaceArea <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidWallSurfaceArea, StatusCodes.Status400BadRequest);
            }
        }
    }
}
