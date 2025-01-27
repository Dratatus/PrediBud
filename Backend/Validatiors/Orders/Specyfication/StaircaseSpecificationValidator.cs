using Backend.Data.Consts;
using Backend.DTO.Specyfication;
using Backend.Middlewares;

namespace Backend.Validatiors.ConstructionOrder.Specyfication
{
    public static class StaircaseSpecificationValidator
    {
        public static void Validate(StaircaseSpecificationDetails details)
        {
            if (details == null)
            {
                throw new ApiException(ErrorMessages.InvalidStaircaseDetails, StatusCodes.Status400BadRequest);
            }

            if (details.Material == null)
            {
                throw new ApiException(ErrorMessages.InvalidMaterialType, StatusCodes.Status400BadRequest);
            }

            if (details.NumberOfSteps <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidStaircaseNumberOfSteps, StatusCodes.Status400BadRequest);
            }

            if (details.Height <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidStaircaseHeight, StatusCodes.Status400BadRequest);
            }

            if (details.Width <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidStaircaseWidth, StatusCodes.Status400BadRequest);
            }
        }
    }
}
