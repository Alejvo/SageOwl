using Domain.Forms;
using Domain.Users;

namespace Domain.FormSubmissions;

public class FormSubmission
{
    private FormSubmission() { }

    private FormSubmission(Guid formId, Guid userId, ResultStatus status)
    {
        FormId = formId;
        UserId = userId;
        Status = status;
    }

    public Guid Id { get; set; }
    public Guid FormId { get; set; }
    public Form Form { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public ResultStatus Status { get; set; }
    public DateTime? SubmittedAt { get; set; }

    private readonly List<Answer> _answers = new();
    public IReadOnlyCollection<Answer> Answers => _answers;

    public static FormSubmission Create(Guid formId, Guid userId)
        => new(formId,userId,ResultStatus.Pending);

    public void AnswerQuestion(Question question, string value)
    {
        var answer = Answer.Create(question.Id, value);

        var existing = _answers.FirstOrDefault(a => a.QuestionId == question.Id);

        if (existing is null)
            _answers.Add(answer);
        else
            existing.ChangeValue(value);
    }
}
