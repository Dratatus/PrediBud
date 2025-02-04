using Backend.Data.Consts;
using Backend.DTO.Specyfication;
using Backend.Middlewares;

namespace Backend.Validatiors.Orders.Specyfication
{
    public static class LoadBearingWallSpecificationValidator
    {
        public static void Validate(LoadBearingWallSpecificationDetails details)
        {
            if (details == null)
            {
                throw new ApiException(ErrorMessages.InvalidLoadBearingWallDetails, StatusCodes.Status400BadRequest);
            }
            if (details.Material == null)
            {
                throw new ApiException(ErrorMessages.InvalidMaterialType, StatusCodes.Status400BadRequest);
            }
            if (details.Height <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidLoadBearingWallHeight, StatusCodes.Status400BadRequest);
            }
            if (details.Width <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidLoadBearingWallWidth, StatusCodes.Status400BadRequest);
            }
            if (details.Thickness <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidLoadBearingWallThickness, StatusCodes.Status400BadRequest);
            }
        }
    }
}
