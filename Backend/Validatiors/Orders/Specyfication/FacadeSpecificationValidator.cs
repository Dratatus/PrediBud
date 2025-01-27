using Backend.Data.Consts;
using Backend.DTO.Specyfication;
using Backend.Middlewares;

namespace Backend.Validatiors.ConstructionOrder.Specyfication
{
    public static class FacadeSpecificationValidator
    {
        public static void Validate(FacadeSpecificationDetails details)
        {
            if (details == null)
            {
                throw new ApiException(ErrorMessages.InvalidFacadeDetails, StatusCodes.Status400BadRequest);
            }

            if (details.InsulationType == null)
            {
                throw new ApiException(ErrorMessages.InvalidMaterialType, StatusCodes.Status400BadRequest);
            }

            if (details.FinishMaterial == null)
            {
                throw new ApiException(ErrorMessages.InvalidFinishMaterial, StatusCodes.Status400BadRequest);
            }

            if (details.SurfaceArea <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidFacadeSurfaceArea, StatusCodes.Status400BadRequest);
            }
        }
    }
}
