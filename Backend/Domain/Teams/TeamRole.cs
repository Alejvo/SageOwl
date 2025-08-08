namespace Domain.Teams;

public class TeamRole
{
    public static readonly TeamRole Admin = new("Admin");
    public static readonly TeamRole Member = new("Member");
    public string Value { get; }
    public TeamRole() { }
    private TeamRole(string value) => Value = value;

    public override string ToString() => Value;

    public static TeamRole FromString(string value)
    {
        return value switch
        {
            "Admin" => Admin,
            "Member" => Member,
            _ => throw new ArgumentException("Invalid Role")
        };
    }
    
}
