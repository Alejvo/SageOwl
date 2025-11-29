namespace Domain.Users.Events;

public class UserCreatedDomainEvent
{
    public string Name { get; } = string.Empty;
    public string Email { get; } = string.Empty;

    public UserCreatedDomainEvent(string name, string email)
    {
        Name = name;
        Email = email;
    }
}
