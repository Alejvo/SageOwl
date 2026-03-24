using Application.Abstractions;
using Application.Teams.Common;

namespace Application.Teams.Create;

public record CreateTeamCommand(
    string Name,
    string Description,
    List<MemberDto> Members
) : ICommand;
