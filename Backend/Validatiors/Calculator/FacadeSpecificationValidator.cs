using Backend.Data.Consts;
using Backend.Data.Models.Constructions.Dimensions.Facade;
using Backend.Middlewares;

namespace Backend.Validatiors.Calculator
{
    public static class FacadeSpecificationValidator
    {
        public static void Validate(FacadeSpecification spec)
        {
            if (spec == null)
            {
                throw new ApiException(ErrorMessages.SpecificationCannotBeNull, StatusCodes.Status400BadRequest);
            }

            if (spec.SurfaceArea <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidFacadeSurfaceArea, StatusCodes.Status400BadRequest);
            }
        }
    }
}
