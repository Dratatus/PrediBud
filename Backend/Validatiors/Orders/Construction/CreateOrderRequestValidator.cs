using Backend.Data.Consts;
using Backend.Data.Models.Constructions;
using Backend.DTO.Request;
using Backend.Middlewares;

namespace Backend.Validatiors.Orders.Construction;
public static class CreateOrderRequestValidator
{
    public static void Validate(CreateOrderRequest request)
    {
        if (request == null)
        {
            throw new ApiException(ErrorMessages.RequestCannotBeNull, StatusCodes.Status400BadRequest);
        }

        if (string.IsNullOrWhiteSpace(request.Description))
        {
            throw new ApiException(ErrorMessages.DescriptionIsRequired, StatusCodes.Status400BadRequest);
        }

        if (request.ClientId <= 0)
        {
            throw new ApiException(ErrorMessages.InvalidClientId, StatusCodes.Status400BadRequest);
        }

        if (request.SpecificationDetails == null)
        {
            throw new ApiException(ErrorMessages.SpecificationDetailsCannotBeNull, StatusCodes.Status400BadRequest);
        }

        if (!Enum.IsDefined(typeof(ConstructionType), request.ConstructionType))
        {
            throw new ApiException(string.Format(ErrorMessages.UnsupportedConstructionType, request.ConstructionType), StatusCodes.Status400BadRequest);
        }
    }
}

