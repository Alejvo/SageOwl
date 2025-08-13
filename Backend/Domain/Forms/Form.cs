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

    private readonly List<Question> _questions = new();
    public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();

    public Form() { }

    private Form(Guid teamId, string title, DateTime deadline)
    {
        Id = Guid.NewGuid();
        TeamId = teamId;
        Title = title;
        Deadline = deadline;
    }

    public static Form Create(Guid teamId, string title,DateTime deadline)
    {
        return new Form(teamId,title,deadline);
    }

    public Question AddQuestion(string questionTitle, string questionDescription, Guid formId, QuestionType questionType)
    {
        var question = Question.Create(formId, questionTitle, questionType ,questionDescription);
        _questions.Add(question);
        return question;
    }
}
