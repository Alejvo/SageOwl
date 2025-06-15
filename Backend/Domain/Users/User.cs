namespace Domain.Users;

public class User
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Surname { get; init; } = string.Empty;
    public Email Email { get; init; } 
    public Password Password { get; init; }
    public string Username {  get; init; } = string.Empty;
    public DateTime Birthday { get; init; }
    public DateTime CreatedAt { get; init; }

    public User()
    {
    }
}
