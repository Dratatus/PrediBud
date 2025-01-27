using Backend.Data.Consts;
using Backend.DTO.MaterialOrder;
using Backend.Middlewares;

namespace Backend.Validatiors.Orders.Material
{
    public static class UpdateMaterialOrderDtoValidator
    {
        public static void Validate(UpdateMaterialOrderDto dto)
        {
            if (dto == null)
            {
                throw new ApiException(ErrorMessages.NullUpdateMaterialOrderDto, StatusCodes.Status400BadRequest);
            }

            if (dto.ID <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidMaterialOrderId, StatusCodes.Status400BadRequest);
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
