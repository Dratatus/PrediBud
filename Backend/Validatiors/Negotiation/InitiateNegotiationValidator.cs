using Backend.Data.Consts;
using Backend.Data.Models.Orders;
using Backend.Middlewares;

namespace Backend.Validatiors.Negotiation
{
    public static class InitiateNegotiationValidator
    {
        public static void Validate(int workerId, decimal proposedPrice)
        {
            if (workerId <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidWorkerId, StatusCodes.Status400BadRequest);
            }

            if (proposedPrice <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidProposedPrice, StatusCodes.Status400BadRequest);
            }
        }
    }
}
