using Domain.Tokens;

namespace Domain.Users;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public Email Email { get; set; }
    public Password Password { get; set; }
    public string Username {  get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
    public DateTime CreatedAt { get; set; }
    public Token? Token { get; set; }

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
