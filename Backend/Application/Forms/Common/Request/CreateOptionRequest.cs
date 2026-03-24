namespace Application.Forms.Common.Request;

public record CreateOptionRequest(
    string Value, 
    bool IsCorrect
    );
