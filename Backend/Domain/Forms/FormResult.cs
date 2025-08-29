using Domain.Users;

namespace Domain.Forms;

public class FormResult
{
    public FormResult() { }

    public FormResult(Guid id,Guid formId, Guid userId, ResultStatus status)
    {
        Id = id;
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
    public List<Answer> Answers { get; set; }

    public static FormResult Create(Guid formId, Guid userId)
        => new(Guid.NewGuid(),formId,userId,ResultStatus.Pending);
}

