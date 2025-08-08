namespace Domain.Teams;

public class Team
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    private readonly List<TeamMembership> _members = new();
    public IReadOnlyCollection<TeamMembership> Members => _members.AsReadOnly();

    private Team(Guid id, string name,string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
    public static Team Create(string name,string description)
    {
        return new Team(Guid.NewGuid(), name, description);
    }

    public void ChangeName(string newName)
    {
        if(string.IsNullOrEmpty(newName))
            throw new ArgumentNullException(nameof(newName));

        Name = newName;
    }

    public void AddMember(Guid userId, TeamRole role)
    {
        _members.Add(TeamMembership.Create(userId, Id, role));
    }

    public void RemoveMember(Guid userId)
    {
        var membership = _members.FirstOrDefault(m => m.UserId == userId);
        if (membership is null)
            throw new InvalidOperationException("User is not a member of the team");

        _members.Remove(membership);
    }

    public void EnsureAtLeastOneAdmin()
    {
        if (!_members.Any(m => m.Role == TeamRole.Admin))
            throw new InvalidOperationException("The team must have at least one admin");
    }
}
