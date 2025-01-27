using Backend.Data.Consts;
using Backend.DTO.Specyfication;
using Backend.Middlewares;

namespace Backend.Validatiors.ConstructionOrder.Specyfication
{
    public static class BalconySpecificationValidator
    {
        public static void Validate(BalconySpecificationDetails details)
        {
            if (details == null)
            {
                throw new ApiException(ErrorMessages.InvalidBalconyDetails, StatusCodes.Status400BadRequest);
            }

            if (details.RailingMaterial == null)
            {
                throw new ApiException(ErrorMessages.InvalidMaterialType, StatusCodes.Status400BadRequest);
            }

            if (details.Length <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidBalconyLength, StatusCodes.Status400BadRequest);
            }

            if (details.Width <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidBalconyWidth, StatusCodes.Status400BadRequest);
            }
        }
    }
}
