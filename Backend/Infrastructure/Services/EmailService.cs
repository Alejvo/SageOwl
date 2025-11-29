using Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("valerie.hahn@ethereal.email"));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = body };

        var host = _configuration["EmailHost"];
        var username = _configuration["EmailUserName"];
        var password = _configuration["EmailPassword"];

        if (host == null || username == null || password == null)
            throw new Exception("Missing email configuration values.");

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(host, 587, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(username, password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
