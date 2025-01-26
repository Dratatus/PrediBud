using Backend.Data.Consts;
using Backend.DTO.Specyfication;
using Backend.Middlewares;

namespace Backend.Validatiors.ConstructionOrder.Specyfication
{
    public static class InsulationOfAtticSpecificationValidator
    {
        public static void Validate(InsulationOfAtticSpecificationDetails details)
        {
            if (details == null)
            {
                throw new ApiException(ErrorMessages.InvalidInsulationOfAtticDetails, StatusCodes.Status400BadRequest);
            }

            if (details.Area <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidInsulationOfAtticArea, StatusCodes.Status400BadRequest);
            }

            if (details.Thickness <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidInsulationOfAtticThickness, StatusCodes.Status400BadRequest);
            }
        }
    }
}
