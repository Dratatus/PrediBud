using Backend.Data.Consts;
using Backend.DTO.Specyfication;
using Backend.Middlewares;

namespace Backend.Validatiors.ConstructionOrder.Specyfication
{
    public static class WindowsSpecificationValidator
    {
        public static void Validate(WindowsSpecificationDetails details)
        {
            if (details == null)
            {
                throw new ApiException(ErrorMessages.InvalidWindowsDetails, StatusCodes.Status400BadRequest);
            }

            if (details.Amount <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidWindowsAmount, StatusCodes.Status400BadRequest);
            }

            if (details.Height <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidWindowsHeight, StatusCodes.Status400BadRequest);
            }

            if (details.Width <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidWindowsWidth, StatusCodes.Status400BadRequest);
            }
        }
    }
}
