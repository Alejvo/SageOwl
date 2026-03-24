using Application.Abstractions;
using Application.Forms.Common.Request;

namespace Application.Forms.Commands.Update;

public record UpdateFormCommand(
    Guid FormId,
    string Title,
    Guid TeamId,
    DateTime Deadline,
    List<UpdateQuestionRequest> Questions
    ) : ICommand;
