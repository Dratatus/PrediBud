using Backend.Data.Consts;
using Backend.Data.Models.Constructions.Specyfication.Chimney;
using Backend.Middlewares;
using System.ComponentModel.DataAnnotations;

namespace Backend.Validatiors.Calculator
{
    public static class ChimneySpecificationValidator
    {
        public static void Validate(ChimneySpecification spec)
        {
            if (spec == null)
            {
                throw new ApiException(ErrorMessages.SpecificationCannotBeNull, StatusCodes.Status400BadRequest);
            }

            if (spec.Count <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidChimneyCount, StatusCodes.Status400BadRequest);
            }
        }
    }
}
