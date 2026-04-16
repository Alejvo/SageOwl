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
        var existing = _questions
            .Where(o => o.Id != Guid.Empty)
            .ToDictionary(o => o.Id);

        var incomingIds = incoming
            .Where(x => x.Id != Guid.Empty)
            .Select(x => x.Id)
            .ToHashSet();

        _questions.RemoveAll(x => !incomingIds.Contains(x.Id));

        foreach (var q in incoming)
        {
            if (q.Id is Guid id && existing.TryGetValue(id, out var entity))
            {
                // Update
                entity.UpdateText(q.Text);

                if (entity is ClosedQuestion closed && q.Options is not null)
                {  
                    closed.SyncOptions(q.Options);
                }
            }
            else
            {
                // Insert
                if(q.Type == "open")
                    AddQuestion(new OpenQuestion(q.Text));

                else if(q.Type == "closed")
                {
                    var cq = new ClosedQuestion(q.Text);
                    AddQuestion(cq);

                    if(q.Options is not null)
                    {
                        foreach (var opt in q.Options)
                        {
                            cq.AddOption(Option.Create(opt.Value, opt.IsCorrect));
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Unknown question type: {q.Type}");
                }
            }
        }
    }

}
