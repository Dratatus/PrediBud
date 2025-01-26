using Backend.Data.Consts;
using Backend.Data.Models.Users;
using Backend.Middlewares;

namespace Backend.Validatiors.Negotiation
{
    public static class AcceptNegotiationValidator
    {
        public static void Validate(User user)
        {
            if (user == null)
            {
                throw new ApiException(ErrorMessages.UserNotFound, StatusCodes.Status404NotFound);
            }

            if (!(user is Client || user is Worker))
            {
                throw new ApiException(ErrorMessages.UnauthorizedAccess, StatusCodes.Status403Forbidden);
            }
        }
    }
}
