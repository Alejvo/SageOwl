using Application.Abstractions;
using Application.Forms.Common.Request;

namespace Application.Forms.Commands.Create;

public record CreateFormCommand(
    string Title,
    Guid TeamId,
    DateTime Deadline,
    List<CreateQuestionRequest> Questions
    ):ICommand;
