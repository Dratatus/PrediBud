using Backend.Data.Consts;
using Backend.DTO.Specyfication;
using Backend.Middlewares;

namespace Backend.Validatiors.ConstructionOrder.Specyfication
{
    public static class FoundationSpecificationValidator
    {
        public static void Validate(FoundationSpecificationDetails details)
        {
            if (details == null)
            {
                throw new ApiException(ErrorMessages.InvalidFoundationDetails, StatusCodes.Status400BadRequest);
            }

            if (details.Length <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidFoundationLength, StatusCodes.Status400BadRequest);
            }

            if (details.Width <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidFoundationWidth, StatusCodes.Status400BadRequest);
            }

            if (details.Depth <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidFoundationDepth, StatusCodes.Status400BadRequest);
            }
        }
    }
}
