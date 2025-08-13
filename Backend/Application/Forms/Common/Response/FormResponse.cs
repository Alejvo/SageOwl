using Domain.Forms;

namespace Application.Forms.Common.Response;

public record FormResponse(
    Guid Id,
    string Title,
    Guid TeamId,
    DateTime Deadline,
    List<FormQuestionResponse> Questions
    );
