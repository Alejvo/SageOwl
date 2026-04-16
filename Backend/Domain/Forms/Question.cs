namespace Domain.Forms;

public abstract class Question
{
    public Guid Id { get; protected set; }
    public string Text { get; protected set; }

    protected Question() { }

    protected Question(string text)
    {
        Text = text;
    }
    public void UpdateText(string newText)
    {
        if (string.IsNullOrWhiteSpace(newText))
            throw new Exception("Question text cannot be empty");

        Text = newText;
    }
}
