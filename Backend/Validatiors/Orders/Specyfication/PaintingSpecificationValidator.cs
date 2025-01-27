using Backend.Data.Consts;
using Backend.DTO.Specyfication;
using Backend.Middlewares;

namespace Backend.Validatiors.ConstructionOrder.Specyfication
{
    public static class PaintingSpecificationValidator
    {
        public static void Validate(PaintingSpecificationDetails details)
        {
            if (details == null)
            {
                throw new ApiException(ErrorMessages.InvalidPaintingDetails, StatusCodes.Status400BadRequest);
            }

            if (details.PaintType == null)
            {
                throw new ApiException(ErrorMessages.InvalidMaterialType, StatusCodes.Status400BadRequest);
            }

            if (details.WallSurfaceArea <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidPaintingWallSurfaceArea, StatusCodes.Status400BadRequest);
            }

            if (details.NumberOfCoats <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidPaintingNumberOfCoats, StatusCodes.Status400BadRequest);
            }
        }
    }
}
