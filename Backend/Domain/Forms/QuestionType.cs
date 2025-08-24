using Domain.Teams;

namespace Domain.Forms;

public class QuestionType
{
    public static readonly QuestionType OpenedEnded = new("OPENED_ENDED");
    public static readonly QuestionType MultipleChoice = new("MULTIPLE_CHOICE");

    public string Value { get; }
    private QuestionType() { }

    public QuestionType(string value) => Value = value;

    public override string ToString() => Value;

    public static QuestionType FromString(string value)
    {
        return value switch
        {
            "OPENED_ENDED" => OpenedEnded,
            "MULTIPLE_CHOICE" => MultipleChoice,
            _ => throw new ArgumentException("Invalid Role")
        };
    }

}
