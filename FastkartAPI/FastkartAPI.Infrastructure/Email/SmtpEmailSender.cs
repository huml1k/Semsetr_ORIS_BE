﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FastkartAPI.Infrastructure.Email
{
    //public class SmtpEmailSender(ILogger<SmtpEmailSender> logger,
    //                   IOptions<MailserverConfiguration> mailserverOptions) : IEmailSender
    //{
    //    private readonly ILogger<SmtpEmailSender> _logger = logger;
    //    private readonly MailserverConfiguration _mailserverConfiguration = mailserverOptions.Value!;

    //    public async Task SendEmailAsync(string to, string from, string subject, string body)
    //    {
    //        var emailClient = new SmtpClient(_mailserverConfiguration.Hostname, _mailserverConfiguration.Port);

    //        var message = new MailMessage
    //        {
    //            From = new MailAddress(from),
    //            Subject = subject,
    //            Body = body
    //        };
    //        message.To.Add(new MailAddress(to));
    //        await emailClient.SendMailAsync(message);
    //        _logger.LogWarning("Sending email to {to} from {from} with subject {subject} using {type}.", to, from, subject, this.ToString());
    //    }
    //}
}
