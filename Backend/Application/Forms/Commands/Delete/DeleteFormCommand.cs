using Application.Abstractions;

namespace Application.Forms.Commands.Delete;

public record DeleteFormCommand(Guid FormId) : ICommand;
