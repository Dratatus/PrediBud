using Backend.Data.Consts;
using Backend.DTO.Specyfication;
using Backend.Middlewares;

namespace Backend.Validatiors.ConstructionOrder.Specyfication
{
    public static class FlooringSpecificationValidator
    {
        public static void Validate(FlooringSpecificationDetails details)
        {
            if (details == null)
            {
                throw new ApiException(ErrorMessages.InvalidFlooringDetails, StatusCodes.Status400BadRequest);
            }

            if (details.Material == null)
            {
                throw new ApiException(ErrorMessages.InvalidMaterialType, StatusCodes.Status400BadRequest);
            }

            if (details.Area <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidFlooringArea, StatusCodes.Status400BadRequest);
            }
        }
    }
}
