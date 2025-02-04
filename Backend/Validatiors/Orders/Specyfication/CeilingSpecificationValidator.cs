using Backend.Data.Consts;
using Backend.DTO.Specyfication;
using Backend.Middlewares;

namespace Backend.Validatiors.Orders.Specyfication
{
    public static class CeilingSpecificationValidator
    {
        public static void Validate(CeilingSpecificationDetails details)
        {
            if (details == null)
            {
                throw new ApiException(ErrorMessages.InvalidCeilingDetails, StatusCodes.Status400BadRequest);
            }
            if (details.Material == null)
            {
                throw new ApiException(ErrorMessages.InvalidMaterialType, StatusCodes.Status400BadRequest);
            }
            if (details.Area <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidCeilingArea, StatusCodes.Status400BadRequest);
            }
        }
    }

}
