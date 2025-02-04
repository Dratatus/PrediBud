using Backend.Data.Consts;
using Backend.Middlewares;

namespace Backend.Validatiors.Negotiation
{
    public static class WorkerNegotiationValidator
    {
        public static void Validate(int orderId, int workerId)
        {
            if (orderId <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidOrderId, StatusCodes.Status400BadRequest);
            }
            if (workerId <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidWorkerId, StatusCodes.Status400BadRequest);
            }
        }
    }
}
