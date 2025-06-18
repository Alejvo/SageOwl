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

    private User(Guid id,string name, string surname, Email email, Password password, string username, DateTime birthday)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
        Password = password ;
        Username = username;
        Birthday = birthday;
        CreatedAt = DateTime.Now;
    }

    public static User Create(Guid id, string name, string surname, string email, string password, string username, DateTime birthday)
    {
        var emailValue = Email.Create(email);
        var passwordValue = Password.Create(password);

        return new User(id,name,surname,emailValue,passwordValue,username,birthday);
    }
}
