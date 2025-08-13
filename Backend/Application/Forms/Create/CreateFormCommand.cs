using Application.Abstractions;
using Application.Forms.Common.Request;

namespace Application.Forms.Create;

public record CreateFormCommand(
    string Title,
    Guid TeamId,
    DateTime Deadline,
    List<FormQuestionRequest> Questions
    ):ICommand;
