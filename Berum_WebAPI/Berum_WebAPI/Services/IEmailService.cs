namespace Berum_WebAPI.Services
{
    public interface IEmailService
    {
        Task SendEmail(string to, string subject, string body);
    }
}
