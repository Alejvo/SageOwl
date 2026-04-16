namespace Application.Forms.Common.Response;

public record ClosedQuestionResponse(
    Guid Id,
    string Text,
    List<OptionResponse> Options
    ) :QuestionResponse(Id,Text);
