using Domain.Forms;
using Domain.Users;

namespace Domain.FormSubmissions;

public class FormSubmission
{
    private FormSubmission() { }

    private FormSubmission(Guid formId, Guid userId, DateTime submittedAt)
    {
        FormId = formId;
        UserId = userId;
        SubmittedAt = submittedAt;
    }

    public Guid Id { get; set; }
    public Guid FormId { get; set; }
    public Form Form { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public DateTime SubmittedAt { get; set; }

    private readonly List<Answer> _answers = new();
    public IReadOnlyCollection<Answer> Answers => _answers;

    public static FormSubmission Create(Guid formId, Guid userId, DateTime submittedAt)
        => new(formId, userId, submittedAt);

    public void AnswerQuestion(Guid questionId, string value)
    {
        var answer = Answer.Create(questionId, value);

        _answers.Add(answer);
    }
}
