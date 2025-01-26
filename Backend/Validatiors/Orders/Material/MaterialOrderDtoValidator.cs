using Backend.Data.Consts;
using Backend.DTO.MaterialOrder;
using Backend.Middlewares;

namespace Backend.Validatiors.Orders.Material
{
    public static class MaterialOrderDtoValidator
    {
        public static void Validate(MaterialOrderDto dto)
        {
            if (dto == null)
            {
                throw new ApiException(ErrorMessages.NullMaterialOrderDto, StatusCodes.Status400BadRequest);
            }

            if (dto.UnitPriceNet <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidUnitPriceNet, StatusCodes.Status400BadRequest);
            }

            if (dto.UnitPriceGross <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidUnitPriceGross, StatusCodes.Status400BadRequest);
            }

            if (dto.Quantity <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidQuantity, StatusCodes.Status400BadRequest);
            }

            if (dto.CreatedDate == default)
            {
                throw new ApiException(ErrorMessages.InvalidCreatedDate, StatusCodes.Status400BadRequest);
            }

            if (dto.UserId <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidUserId, StatusCodes.Status400BadRequest);
            }

            if (dto.SupplierId <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidSupplierId, StatusCodes.Status400BadRequest);
            }

            if (dto.MaterialPriceId.HasValue && dto.MaterialPriceId <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidMaterialPriceId, StatusCodes.Status400BadRequest);
            }
        }
    }
}
