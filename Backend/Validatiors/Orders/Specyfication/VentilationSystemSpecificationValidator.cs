using Backend.Data.Consts;
using Backend.DTO.Specyfication;
using Backend.Middlewares;

namespace Backend.Validatiors.Orders.Specyfication
{
    public static class VentilationSystemSpecificationValidator
    {
        public static void Validate(VentilationSystemSpecificationDetails details)
        {
            if (details == null)
            {
                throw new ApiException(ErrorMessages.InvalidVentilationSystemDetails, StatusCodes.Status400BadRequest);
            }
            if (details.Count <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidVentilationSystemCount, StatusCodes.Status400BadRequest);
            }
        }
    }

}
