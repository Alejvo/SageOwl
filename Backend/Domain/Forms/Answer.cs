using Domain.Users;

namespace Domain.Forms;

public class Answer
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public Guid SelectedOptionId {  get; set; }
    public string? OpenQuestionAnswer { get; set; }

    private Answer() { }

    public Answer(Guid questionId, Guid selectedOptionId, string? openQuestionAnswer)
    {
        Id = Guid.NewGuid();
        QuestionId = questionId;
        SelectedOptionId = selectedOptionId;
        OpenQuestionAnswer = openQuestionAnswer;
    }
}
