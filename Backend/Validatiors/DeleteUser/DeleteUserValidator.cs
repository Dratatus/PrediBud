using Backend.Data.Consts;
using Backend.Data.Models.Users;
using Backend.Middlewares;

namespace Backend.Validatiors.DeleteUser
{
    public static class DeleteUserValidator
    {
        public static void Validate(int userId)
        {
            if (userId <= 0)
            {
                throw new ApiException(ErrorMessages.InvalidUserId, StatusCodes.Status400BadRequest);
            }
        }

        public static void ValidateUserOrders(User user, bool hasMaterialOrders, bool hasConstructionOrders)
        {
            if (hasMaterialOrders)
            {
                throw new ApiException(ErrorMessages.UserHasMaterialOrders, StatusCodes.Status400BadRequest);
            }

            if (hasConstructionOrders)
            {
                throw new ApiException(ErrorMessages.UserHasConstructionOrders, StatusCodes.Status400BadRequest);
            }
        }
    }

}
