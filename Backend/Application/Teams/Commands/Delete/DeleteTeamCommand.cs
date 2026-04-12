using Application.Abstractions;

namespace Application.Teams.Commands.Delete;

public record DeleteTeamCommand(Guid TeamId):ICommand;

