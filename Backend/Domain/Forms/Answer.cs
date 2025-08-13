using Domain.Users;

namespace Domain.Forms;

public class Answer
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid QuestionId { get; set; }
    public Guid SelectedOptionId {  get; set; }
    public string? OpenQuestionAnswer { get; set; }

    private Answer() { }

    public Answer(Guid userId, Guid questionId, Guid selectedOptionId, string? openQuestionAnswer)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        QuestionId = questionId;
        SelectedOptionId = selectedOptionId;
        OpenQuestionAnswer = openQuestionAnswer;
    }
}
