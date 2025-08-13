namespace Application.Forms.Common.Response;

public record FormQuestionResponse(
    string Title,
    string? Description,
    List<FormOptionResponse> Options
    );
