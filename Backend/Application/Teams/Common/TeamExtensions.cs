using Domain.Announcements;
using Domain.Teams;

namespace Application.Teams.Common;

public static class TeamExtensions
{
    public static TeamResponse ToTeamResponse(this Team team)
    {
        return new TeamResponse
        (
            team.Id,
            team.Name,
            team.Description,
            team.Members.Select(m => new TeamMemberResponse
            (
                m.UserId,
                m.User.Name,
                m.User.Surname,
                m.User.Email.Value,
                m.User.Username,
                m.User.CreatedAt,
                m.Role.Value
            )).ToList(),
            team.Forms.Select(f => new FormDto(
                f.Id,
                f.Title,
                f.Deadline
            )).ToList(),
            team.Announcements.Select(a => new AnnouncementDto(
                a.Title,
                a.Content,
                a.CreatedAt,
                a.Author.Name
             )).ToList()
        );
    }
}
