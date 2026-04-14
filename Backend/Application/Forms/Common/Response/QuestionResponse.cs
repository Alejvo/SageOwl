namespace Application.Forms.Common.Response;

public abstract record QuestionResponse(
    Guid Id,
    string Text,
    string Type
    );
