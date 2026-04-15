namespace Domain.Forms.Dtos;

public record QuestionInput(
    string Text,
    string Type,
    IEnumerable<OptionInput> Options,
    Guid? Id = null
    );
