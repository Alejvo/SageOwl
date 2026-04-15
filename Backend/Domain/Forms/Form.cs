using Domain.Forms.Dtos;
using Domain.Qualifications;
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

    public void Update(string title, DateTime deadline)
    {
        Title = title;
        Deadline = deadline;
    }

    public void AddQuestion(Question question)
    {
        ArgumentNullException.ThrowIfNull(question);

        if (question is ClosedQuestion closed && closed.Options.Count == 0)
            throw new Exception("Closed question must have options");

        _questions.Add(question);
    }

    public void SyncQuestions(IEnumerable<QuestionInput> incoming)
    {
        var existing = _questions.ToDictionary(x => x.Id);

        var incomingIds = incoming
            .Where(x => x.Id != Guid.Empty)
            .Select(x => x.Id)
            .ToHashSet();

        _questions.RemoveAll(x =>
            x.Id != Guid.Empty && !incomingIds.Contains(x.Id));

        foreach (var q in incoming)
        {
            if (q.Id != Guid.Empty && existing.TryGetValue(q.Id.Value, out var entity))
            {
                // Update
                entity.UpdateText(q.Text);

                if (entity is ClosedQuestion closed)
                {  
                    closed.SyncOptions(q.Options);
                }
            }
            else
            {
                // Insert
                if(q.Type == "Open")
                    AddQuestion(new OpenQuestion(q.Text));

                else if(q.Type == "Closed")
                {
                    var cq = new ClosedQuestion(q.Text);
                    AddQuestion(cq);
                    foreach(var opt in q.Options)
                    {
                        cq.AddOption(Option.Create(opt.Value,opt.IsCorrect));
                    }
                }
            }
        }
    }

}
