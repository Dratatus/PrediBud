using Backend.Data.Consts;
using Backend.DTO.Specyfication.Backend.DTO.Specifications;
using Backend.Middlewares;

namespace Backend.Validatiors.Orders.Specyfication
{
    public static class PartitionWallSpecificationValidator
    {
        public static void Validate(PartitionWallSpecificationDetails details)
        {
            if (details == null)
            {
                throw new ApiException(ErrorMessages.InvalidPartitionWallDetails, StatusCodes.Status400BadRequest);
            }
            if (details.Material == null)
            {
                throw new ApiException(ErrorMessages.InvalidMaterialType, StatusCodes.Status400BadRequest);
            }
            if (details.Height <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidPartitionWallHeight, StatusCodes.Status400BadRequest);
            }
            if (details.Width <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidPartitionWallWidth, StatusCodes.Status400BadRequest);
            }
            if (details.Thickness <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidPartitionWallThickness, StatusCodes.Status400BadRequest);
            }
        }
    }

}
