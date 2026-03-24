using Domain.Teams;
using Domain.Users;
using System.Data;

namespace Domain.Forms;

public class Form
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid TeamId { get; set; }
    public Team Team { get; set; }
    public DateTime Deadline { get; set; }

    private readonly List<FormResult> _results = new();
    public IReadOnlyCollection<FormResult> Results => _results;

    private readonly List<Question> _questions = new();
    public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();

    public Form() { }

    private Form(Guid id,Guid teamId, string title, DateTime deadline)
    {
        Id = id;
        TeamId = teamId;
        Title = title;
        Deadline = deadline;
    }

    public static Form Create(Guid teamId, string title,DateTime deadline)
    {
        return new Form(Guid.NewGuid(),teamId, title, deadline);
    }

    public void Update(string title, DateTime deadline)
    {
        Title = title;
        Deadline = deadline;
    }

    public void AddResults(List<Guid> members)
    {
        foreach (var member in members)
        {
            _results.Add(FormResult.Create(Id,member));
        }
    }

    public Question AddQuestion(string questionTitle, string questionDescription, Guid formId, QuestionType questionType)
    {
        var existingQuestion = _questions.FirstOrDefault(q => q.FormId == formId);
        if (existingQuestion != null)
        {
            return existingQuestion;
        }

        var question = Question.Create(formId, questionTitle, questionType ,questionDescription);
        _questions.Add(question);
        return question;
    }

    public Question UpdateQuestion(Guid questionId, string title, string description, QuestionType type)
    {
        var question = _questions.FirstOrDefault(q => q.Id == questionId);
        if (question != null)
        {
            question.Update(title, type, description);
        }

        return question;
    }

    public void RemoveQuestion(Guid questionId)
    {
        var question = _questions.FirstOrDefault(q => q.Id == questionId);
        if (question != null)
        {
            _questions.Remove(question);
        }
    }
}
