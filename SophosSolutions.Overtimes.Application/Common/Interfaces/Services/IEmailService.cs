namespace SophosSolutions.Overtimes.Application.Common.Interfaces.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body, bool isHtmlBody = false);
}
