namespace Domain.FormSubmissions;

public class Answer
{
    public Guid Id { get; private set; }
    public Guid QuestionId { get; private set; }
    public string Value { get; private set; }

    private Answer() { }

    public Answer(Guid questionId, string value)
    {
        Id = Guid.NewGuid();
        QuestionId = questionId;
        Value = value;
    }

    public void ChangeValue(string newValue)
    {
        Value = newValue;
    }
}
