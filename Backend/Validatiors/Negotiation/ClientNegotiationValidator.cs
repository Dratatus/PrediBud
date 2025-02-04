using Backend.Data.Consts;
using Backend.Middlewares;

namespace Backend.Validatiors.Negotiation
{
    public static class ClientNegotiationValidator
    {
        public static void Validate(int orderId, int clientId)
        {
            if (orderId <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidOrderId, StatusCodes.Status400BadRequest);
            }
            if (clientId <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidClientId, StatusCodes.Status400BadRequest);
            }
        }
    }
}
