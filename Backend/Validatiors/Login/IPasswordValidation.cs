namespace Backend.Validatiors.Login
{
    public interface IPasswordValidation
    {
        bool ValidatePassword(string password, string passwordHash);
    }
}
