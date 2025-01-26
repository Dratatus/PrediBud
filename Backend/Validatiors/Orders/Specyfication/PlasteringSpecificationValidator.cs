using Backend.Data.Consts;
using Backend.DTO.Specyfication;
using Backend.Middlewares;

namespace Backend.Validatiors.ConstructionOrder.Specyfication
{
    public static class PlasteringSpecificationValidator
    {
        public static void Validate(PlasteringSpecificationDetails details)
        {
            if (details == null)
            {
                throw new ApiException(ErrorMessages.InvalidPlasteringDetails, StatusCodes.Status400BadRequest);
            }

            if (details.WallSurfaceArea <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidPlasteringWallSurfaceArea, StatusCodes.Status400BadRequest);
            }
        }
    }
}
