using Backend.Data.Consts;
using Backend.DTO.Specyfication;
using Backend.Middlewares;

namespace Backend.Validatiors.Orders.Specyfication
{
    public static class RoofSpecificationValidator
    {
        public static void Validate(RoofSpecificationDetails details)
        {
            if (details == null)
            {
                throw new ApiException(ErrorMessages.InvalidRoofDetails, StatusCodes.Status400BadRequest);
            }
            if (details.Material == null)
            {
                throw new ApiException(ErrorMessages.InvalidMaterialType, StatusCodes.Status400BadRequest);
            }
            if (details.Area <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidRoofArea, StatusCodes.Status400BadRequest);
            }
            if (details.Pitch < 0 || details.Pitch > 90)
            {
                throw new ApiException(ErrorMessages.InvalidRoofPitch, StatusCodes.Status400BadRequest);
            }
        }
    }

}
