namespace Application.Forms.Common.Request;

public record UpdateOptionRequest(
    Guid? OptionId,
    string Value,
    bool IsCorrect,
    bool IsDeleted = false
    );
