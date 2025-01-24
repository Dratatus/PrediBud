using Backend.Data.Consts;
using Backend.DTO.Auth;
using Backend.Middlewares;
using System.ComponentModel.DataAnnotations;

namespace Backend.Validatiors.Login
{
    public static class LoginValidator
    {
        public static void Validate(LoginBody request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ApiException(ErrorMessages.EmailRequired, StatusCodes.Status400BadRequest);

            if (!IsValidEmail(request.Email))
                throw new ApiException(ErrorMessages.InvalidEmailFormat, StatusCodes.Status400BadRequest);

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new ApiException(ErrorMessages.PasswordRequired, StatusCodes.Status400BadRequest);
        }

        private static bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}
