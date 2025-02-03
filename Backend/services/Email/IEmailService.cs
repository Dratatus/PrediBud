namespace Backend.services.Email
{
    public interface IEmailService
    {
        Task SendErrorReportAsync(Exception ex);
    }
}
