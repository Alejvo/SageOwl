namespace Application.Forms.Common.Response;

public record FormQuestionResponse(
    string Title,
    string? Description,
    string Type,
    List<FormOptionResponse> Options
    );
