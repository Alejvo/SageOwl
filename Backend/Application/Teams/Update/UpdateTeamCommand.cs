using Application.Abstractions;
using Application.Teams.Common;

namespace Application.Teams.Update;

public record UpdateTeamCommand(
    Guid TeamId,
    string Name,
    string Description,
    List<MemberDto> Members
) : ICommand;
