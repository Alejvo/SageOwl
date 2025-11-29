using MediatR;

namespace Application.Users.Events;

public class UserCreatedNotification : INotification
{
    public string Name { get; } = string.Empty;
    public string Email { get; } = string.Empty;

    public UserCreatedNotification(string name, string email)
    {
        Name = name;
        Email = email;
    }
}
