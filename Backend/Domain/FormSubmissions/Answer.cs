namespace Domain.FormSubmissions;

public class Answer
{
    public Guid Id { get; private set; }
    public Guid QuestionId { get; private set; }
    public string Value { get; private set; }

    private Answer() { }

    private Answer(Guid questionId, string value)
    {
        Id = Guid.NewGuid();
        QuestionId = questionId;
        Value = value;
    }

    public void ChangeValue(string newValue)
    {
        Value = newValue;
    }

    public static Answer Create(Guid questionId, string value)
        => new(questionId, value);
}
