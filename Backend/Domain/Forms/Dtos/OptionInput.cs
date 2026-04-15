namespace Domain.Forms.Dtos;

public record OptionInput(
    string Value,
    bool IsCorrect,
    Guid? Id = null
    );
