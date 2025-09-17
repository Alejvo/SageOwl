namespace SageOwl.UI.Models;

public class Member
{
    public Guid Id {  get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string Role { get; set; }
}
