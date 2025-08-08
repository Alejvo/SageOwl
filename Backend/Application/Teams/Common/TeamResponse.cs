using Application.Users.Common;
using Domain.Teams;

namespace Application.Teams.Common;

public record TeamResponse(
    Guid TeamId,
    string Name,
    string Description,
    List<TeamMemberResponse> Members
);
