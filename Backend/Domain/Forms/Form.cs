using Domain.Teams;

namespace Domain.Forms;

public class Form
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public Guid TeamId { get; private set; }
    public Team Team { get; set; }
    public DateTime Deadline { get; private set; }

    private readonly List<Question> _questions = new();
    public IReadOnlyCollection<Question> Questions => _questions;

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

    public void UpdateTitle(string title) => Title = title;

    public void AddQuestion(Question question)
    {
        ArgumentNullException.ThrowIfNull(question);

        if (question is ClosedQuestion closed && closed.Options.Count == 0)
            throw new Exception("Closed question must have options");

        _questions.Add(question);
    }
}
