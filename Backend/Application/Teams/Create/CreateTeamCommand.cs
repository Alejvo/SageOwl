using Application.Abstractions;
using Application.Teams.Common;
using Domain.Teams;

namespace Application.Teams.Create;

public record CreateTeamCommand(
    string Name,
    string Description,
    List<MemberDto> Members
) : ICommand;
