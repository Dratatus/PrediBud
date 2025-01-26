using Backend.Data.Consts;
using Backend.DTO.Specyfication;
using Backend.Middlewares;

namespace Backend.Validatiors.ConstructionOrder.Specyfication
{
    public static class DoorsSpecificationValidator
    {
        public static void Validate(DoorsSpecificationDetails details)
        {
            if (details == null)
            {
                throw new ApiException(ErrorMessages.InvalidDoorsDetails, StatusCodes.Status400BadRequest);
            }

            if (details.Amount <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidDoorsAmount, StatusCodes.Status400BadRequest);
            }

            if (details.Height <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidDoorsHeight, StatusCodes.Status400BadRequest);
            }

            if (details.Width <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidDoorsWidth, StatusCodes.Status400BadRequest);
            }
        }
    }

}
