namespace Backend.Validation
{
    public class PasswordValidation: IPasswordValidation
    {
        public bool ValidatePassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
