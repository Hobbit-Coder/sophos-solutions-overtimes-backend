using Microsoft.Extensions.Configuration;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Services;
using System.Net;
using System.Net.Mail;

namespace SophosSolutions.Overtimes.Infrastructure.Services;

public class SmtpService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    private readonly IConfiguration _configuration;

    public SmtpService(IConfiguration configuration)
    {
        _configuration = configuration;
        _smtpClient = new SmtpClient(configuration["SmtpService:host"], Convert.ToInt32(configuration["SmtpService:port"]));
        _smtpClient.EnableSsl = Convert.ToBoolean(configuration["SmtpService:ssl"]);
        _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        _smtpClient.Credentials = new NetworkCredential(configuration["SmtpService:email"], configuration["SmtpService:password"]);
    }

    public Task SendEmailAsync(string to, string subject, string body, bool isHtmlBody = false)
    {
        return _smtpClient.SendMailAsync(new MailMessage(_configuration["SmtpService:email"]!, to, subject, body) { IsBodyHtml = isHtmlBody });
    }
}
