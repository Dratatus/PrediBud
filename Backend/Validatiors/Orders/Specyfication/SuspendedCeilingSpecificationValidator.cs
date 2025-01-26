using Backend.Data.Consts;
using Backend.DTO.Specyfication;
using Backend.Middlewares;

namespace Backend.Validatiors.ConstructionOrder.Specyfication
{
    public static class SuspendedCeilingSpecificationValidator
    {
        public static void Validate(SuspendedCeilingSpecificationDetails details)
        {
            if (details == null)
            {
                throw new ApiException(ErrorMessages.InvalidSuspendedCeilingDetails, StatusCodes.Status400BadRequest);
            }

            if (details.Area <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidSuspendedCeilingArea, StatusCodes.Status400BadRequest);
            }

            if (details.Height <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidSuspendedCeilingHeight, StatusCodes.Status400BadRequest);
            }
        }
    }

}
