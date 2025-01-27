using Backend.Data.Consts;
using Backend.Data.Models.Constructions.Specyfication.Ceiling;
using Backend.Middlewares;
using System.ComponentModel.DataAnnotations;

namespace Backend.Validatiors.Calculator
{
    public static class SuspendedCeilingSpecificationValidator
    {
        public static void Validate(SuspendedCeilingSpecification spec)
        {
            if (spec == null)
            {
                throw new ApiException(ErrorMessages.SpecificationCannotBeNull, StatusCodes.Status400BadRequest);
            }

            if (spec.Area <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidSuspendedCeilingArea, StatusCodes.Status400BadRequest);
            }

            if (spec.Height <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidSuspendedCeilingHeight, StatusCodes.Status400BadRequest);
            }
        }
    }
}
