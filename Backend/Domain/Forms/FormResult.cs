using Domain.Users;

namespace Domain.Forms;

public class FormResult
{
    public Guid Id { get; set; }
    public Guid FormId { get; set; }
    public Form Form { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public bool IsPending { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

}
