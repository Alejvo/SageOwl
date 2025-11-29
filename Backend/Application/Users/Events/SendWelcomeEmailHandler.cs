using MediatR;

namespace Application.Users.Events;

public sealed class SendWelcomeEmailHandler : INotificationHandler<UserCreatedNotification>
{
    public async Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Email send to: {notification.Email}");

        await Task.CompletedTask;
    }
}
