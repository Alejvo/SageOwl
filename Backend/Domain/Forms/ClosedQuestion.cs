namespace Domain.Forms;

public class ClosedQuestion : Question
{
    private readonly List<Option> _options;

    public IReadOnlyCollection<Option> Options => [.. _options];

    public ClosedQuestion() { }
    public ClosedQuestion(string text) : base(text)
    {
    }

    public void AddOption(Option option)
    {
        option.SetQuestion(this);
        _options.Add(option);
    }
}
