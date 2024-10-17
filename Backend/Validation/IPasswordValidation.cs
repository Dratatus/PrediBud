namespace Backend.Validation
{
    public interface IPasswordValidation
    {
        bool ValidatePassword(string password, string passwordHash);
    }
}
