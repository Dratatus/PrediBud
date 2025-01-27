using Backend.Data.Consts;
using Backend.DTO.Specyfication;
using Backend.Middlewares;

namespace Backend.Validatiors.ConstructionOrder.Specyfication
{
    public static class ShellOpenSpecificationValidator
    {
        public static void Validate(ShellOpenSpecificationDetails details)
        {
            if (details == null)
            {
                throw new ApiException(ErrorMessages.InvalidShellOpenDetails, StatusCodes.Status400BadRequest);
            }

            if (details.FoundationLength <= 0 || details.FoundationWidth <= 0 || details.FoundationDepth <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidShellOpenFoundation, StatusCodes.Status400BadRequest);
            }

            if (details.LoadBearingWallHeight <= 0 || details.LoadBearingWallWidth <= 0 || details.LoadBearingWallThickness <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidShellOpenLoadBearingWall, StatusCodes.Status400BadRequest);
            }
            if (details.LoadBearingWallMaterial == null)
            {
                throw new ApiException(ErrorMessages.InvalidLoadBearingWallMaterial, StatusCodes.Status400BadRequest);
            }

            if (details.PartitionWallHeight <= 0 || details.PartitionWallWidth <= 0 || details.PartitionWallThickness <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidShellOpenPartitionWall, StatusCodes.Status400BadRequest);
            }
            if (details.PartitionWallMaterial == null)
            {
                throw new ApiException(ErrorMessages.InvalidPartitionWallMaterial, StatusCodes.Status400BadRequest);
            }

            if (details.ChimneyCount <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidShellOpenChimney, StatusCodes.Status400BadRequest);
            }

            if (details.VentilationSystemCount <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidShellOpenVentilation, StatusCodes.Status400BadRequest);
            }

            if (details.CeilingArea <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidShellOpenCeiling, StatusCodes.Status400BadRequest);
            }
            if (details.CeilingMaterial == null)
            {
                throw new ApiException(ErrorMessages.InvalidCeilingMaterial, StatusCodes.Status400BadRequest);
            }

            if (details.RoofArea <= 0 || details.RoofPitch <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidShellOpenRoof, StatusCodes.Status400BadRequest);
            }
            if (details.RoofMaterial == null)
            {
                throw new ApiException(ErrorMessages.InvalidRoofMaterial, StatusCodes.Status400BadRequest);
            }
        }
    }
}
