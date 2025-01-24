using Backend.Data.Consts;
using Backend.Data.Models.Constructions.Specyfication.ShellOpen;
using Backend.Middlewares;

namespace Backend.Validatiors.Calculator
{
    public static class ShellOpenSpecificationValidator
    {
        public static void Validate(ShellOpenSpecification spec)
        {
            if (spec == null)
            {
                throw new ApiException(ErrorMessages.SpecificationCannotBeNull, StatusCodes.Status400BadRequest);
            }

            if (spec.FoundationSpecification != null)
            {
                FoundationSpecificationValidator.Validate(spec.FoundationSpecification);
            }

            if (spec.LoadBearingWallMaterial != null)
            {
                LoadBearingWallSpecificationValidator.Validate(spec.LoadBearingWallMaterial);
            }

            if (spec.PartitionWall != null)
            {
                PartitionWallSpecificationValidator.Validate(spec.PartitionWall);
            }

            if (spec.Chimney != null)
            {
                ChimneySpecificationValidator.Validate(spec.Chimney);
            }

            if (spec.Ventilation != null)
            {
                VentilationSystemSpecificationValidator.Validate(spec.Ventilation);
            }

            if (spec.Celling != null)
            {
                CeilingSpecificationValidator.Validate(spec.Celling);
            }

            if (spec.Roof != null)
            {
                RoofSpecificationValidator.Validate(spec.Roof);
            }
        }
    }

}
