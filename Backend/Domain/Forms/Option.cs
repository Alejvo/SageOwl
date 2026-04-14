namespace Domain.Forms;

public class Option
{
    public Guid Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public Guid QuestionId { get; set; }
    public bool IsCorrect { get; set; }
    public ClosedQuestion Question { get; set; }

    private Option() { }

    private Option(string value, bool isCorrect)
    {
        Id = Guid.NewGuid();
        Value = value;
        IsCorrect = isCorrect;
    }

    internal void SetQuestion(ClosedQuestion question)
    {
        Question = question;
    }

    public static Option Create(string value, bool isCorrect)
        => new Option(value,isCorrect);
}
