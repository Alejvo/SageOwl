using Domain.Teams;

namespace Domain.Forms;

public class QuestionType
{
    public static readonly QuestionType OpenQuestion = new("Open");
    public static readonly QuestionType MultipleChoice = new("Closed");

    public string Value { get; }
    private QuestionType() { }

    public QuestionType(string value) => Value = value;

    public override string ToString() => Value;

    public static QuestionType FromString(string value)
    {
        return value switch
        {
            "Open" => OpenQuestion,
            "Closed" => MultipleChoice,
            _ => throw new ArgumentException("Invalid Role")
        };
    }

}
