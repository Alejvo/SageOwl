namespace SageOwl.UI.Models;

public class CurrentUser
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public DateTime CreatedAt { get; set; }
}
