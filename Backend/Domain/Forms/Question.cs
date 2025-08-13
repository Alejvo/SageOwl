namespace Domain.Forms;

public class Question
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    public Guid FormId { get; set; }
    public Form Form { get; set; }

    public QuestionType QuestionType { get; set; }

    private readonly List<Option> _options = new();
    public IReadOnlyCollection<Option> Options => _options.AsReadOnly();

    public Question() { }

    private Question(string title, string description, Guid formId,QuestionType questionType)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        FormId = formId;
        QuestionType = questionType;
    }

    public static Question Create(Guid formId, string title,QuestionType questionType,string? description)
        => new(title, description,formId,questionType);

    public void AddOption(string value, bool isCorrect)
    {
        var option = Option.Create(value, isCorrect,Id);
        _options.Add(option);
    }
}
