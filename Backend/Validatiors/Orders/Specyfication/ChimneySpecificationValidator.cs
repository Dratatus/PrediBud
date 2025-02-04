using Backend.Data.Consts;
using Backend.DTO.Specyfication;
using Backend.Middlewares;

namespace Backend.Validatiors.Orders.Specyfication
{
    public static class ChimneySpecificationValidator
    {
        public static void Validate(ChimneySpecificationDetails details)
        {
            if (details == null)
            {
                throw new ApiException(ErrorMessages.InvalidChimneyDetails, StatusCodes.Status400BadRequest);
            }
            if (details.Count <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidChimneyCount, StatusCodes.Status400BadRequest);
            }
        }
    }

}
