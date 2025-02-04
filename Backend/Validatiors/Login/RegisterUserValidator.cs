using Backend.Data.Consts;
using Backend.DTO.Auth;
using Backend.Middlewares;

namespace Backend.Validatiors.Login
{
    public static class RegisterUserValidator
    {
        public static void Validate(RegisterUserBody request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ApiException(ErrorMessages.EmailRequired, StatusCodes.Status400BadRequest);

            if (!IsValidEmail(request.Email))
                throw new ApiException(ErrorMessages.InvalidEmailFormat, StatusCodes.Status400BadRequest);

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new ApiException(ErrorMessages.PasswordRequired, StatusCodes.Status400BadRequest);

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ApiException(ErrorMessages.NameRequired, StatusCodes.Status400BadRequest);

            if (string.IsNullOrWhiteSpace(request.Phone))
                throw new ApiException(ErrorMessages.PhoneRequired, StatusCodes.Status400BadRequest);

            if (request.Address == null)
                throw new ApiException(ErrorMessages.AddressRequired, StatusCodes.Status400BadRequest);

            if (string.IsNullOrWhiteSpace(request.Address.City))
                throw new ApiException(ErrorMessages.CityRequired, StatusCodes.Status400BadRequest);

            if (string.IsNullOrWhiteSpace(request.Address.PostCode))
                throw new ApiException(ErrorMessages.PostCodeRequired, StatusCodes.Status400BadRequest);

            if (string.IsNullOrWhiteSpace(request.Address.StreetName))
                throw new ApiException(ErrorMessages.StreetNameRequired, StatusCodes.Status400BadRequest);
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }

}
