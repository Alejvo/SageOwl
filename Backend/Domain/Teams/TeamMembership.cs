using Domain.Users;

namespace Domain.Teams;

public class TeamMembership
{
    public int Id { get; set; }
    public Guid UserId {  get;  private set; }
    public Guid TeamId { get; private set; }
    public TeamRole Role { get; private set; }
    public DateTime JoinedAt {  get; private set; } = DateTime.UtcNow;

    public User User {  get; private set; }
    public Team Team { get; private set; }

    private TeamMembership() { }
    private TeamMembership(Guid userId, Guid teamId, TeamRole role, DateTime joinedAt)
    {
        UserId = userId;
        TeamId = teamId;
        Role = role;
        JoinedAt = joinedAt;
    }

    public static TeamMembership Create(Guid userId, Guid teamId, TeamRole role)
    {
        return new TeamMembership(userId, teamId, role, DateTime.UtcNow);
    }

    public void ChangeRole(TeamRole newRole)
    {
        Role = newRole;
    }
}
