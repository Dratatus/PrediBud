using Backend.Data.Consts;
using Backend.Data.Models.Constructions.Specyfication.Windows;
using Backend.Middlewares;

namespace Backend.Validatiors.Calculator
{
    public static class WindowsSpecificationValidator
    {
        public static void Validate(WindowsSpecification spec)
        {
            if (spec == null)
            {
                throw new ApiException(ErrorMessages.SpecificationCannotBeNull, StatusCodes.Status400BadRequest);
            }

            if (spec.Amount <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidWindowAmount, StatusCodes.Status400BadRequest);
            }

            if (spec.Height <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidWindowHeight, StatusCodes.Status400BadRequest);
            }

            if (spec.Width <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidWindowWidth, StatusCodes.Status400BadRequest);
            }
        }
    }

}
