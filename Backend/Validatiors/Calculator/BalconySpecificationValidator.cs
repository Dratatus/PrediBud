using Backend.Data.Consts;
using Backend.Data.Models.Constructions.Dimensions.Balcony;
using Backend.Middlewares;
using System.ComponentModel.DataAnnotations;

namespace Backend.Validatiors.Calculator
{
    public static class BalconySpecificationValidator
    {
        public static void Validate(BalconySpecification spec)
        {
            if (spec == null)
            {
                throw new ApiException(ErrorMessages.SpecificationCannotBeNull, StatusCodes.Status400BadRequest);
            }

            if (spec.Length <= 0 || spec.Width <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidBalconyDimensions, StatusCodes.Status400BadRequest);
            }
        }
    }
}
