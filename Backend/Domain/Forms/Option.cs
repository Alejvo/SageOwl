namespace Domain.Forms;

public class Option
{
    public Guid Id { get; private set; }
    public string Value { get; private set; } = string.Empty;
    public Guid QuestionId { get; private set; }
    public bool IsCorrect { get; private set; }
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
        QuestionId = question.Id;
    }

    public static Option Create(string value, bool isCorrect)
        => new Option(value,isCorrect);

    public void Update(string value, bool isCorrect)
    {
        Value = value;
        IsCorrect = isCorrect;
    }
}
