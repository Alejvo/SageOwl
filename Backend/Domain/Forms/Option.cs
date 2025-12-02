namespace Domain.Forms;

public class Option
{
    public Guid Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public Guid QuestionId { get; set; }
    public Question Question { get; set; } = new();

    public Option() { }
    private Option(string value, bool isCorrect,Guid questionId)
    {
        Id = Guid.NewGuid();
        Value = value;
        IsCorrect = isCorrect;
        QuestionId = questionId;
    }

    public static Option Create(string value, bool isCorrect, Guid questionId)
        => new(value, isCorrect,questionId);

    public void Update(string value, bool isCorrect)
    {
        Value = value;
        IsCorrect = isCorrect;
    }
}
