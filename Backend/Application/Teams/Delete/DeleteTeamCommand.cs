using Application.Abstractions;

namespace Application.Teams.Delete;

public record DeleteTeamCommand(Guid TeamId):ICommand;

