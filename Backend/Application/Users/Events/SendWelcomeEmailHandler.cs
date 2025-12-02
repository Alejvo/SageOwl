using Application.Abstractions;
using Application.Interfaces;
using MediatR;

namespace Application.Users.Events;

public sealed class SendWelcomeEmailHandler : INotificationHandler<UserCreatedNotification>
{
    private readonly IEmailService _emailService;

    public SendWelcomeEmailHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
    {
        var body = EmailTemplates.WelcomeEmail;
        await _emailService.SendEmailAsync(notification.Email,"Welcome to SageOwl",body);
    }
}
