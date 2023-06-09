using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SophosSolutions.Overtimes.Application.Common.Interfaces.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body, bool isHtmlBody = false);
}
