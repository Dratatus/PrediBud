using Backend.Data.Consts;
using Backend.Data.Models.Constructions.Specyfication.Painting;
using Backend.Middlewares;

namespace Backend.Validatiors.Calculator
{
    public static class PaintingSpecificationValidator
    {
        public static void Validate(PaintingSpecification spec)
        {
            if (spec == null)
            {
                throw new ApiException(ErrorMessages.SpecificationCannotBeNull, StatusCodes.Status400BadRequest);
            }

            if (spec.WallSurfaceArea <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidPaintingSurfaceArea, StatusCodes.Status400BadRequest);
            }

            if (spec.NumberOfCoats <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidNumberOfCoats, StatusCodes.Status400BadRequest);
            }
        }
    }

}
