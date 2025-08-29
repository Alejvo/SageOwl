namespace Domain.Forms;

public class ResultStatus
{
    public static readonly ResultStatus Pending = new("PENDING");
    public static readonly ResultStatus Sent = new("SENT");
    public string Value { get; }

    public ResultStatus() { }

    public ResultStatus(string value) => Value = value;

    public override string ToString() => Value;

    public static ResultStatus FromString(string value)
    {
        return value switch
        {
            "PENDING" => Pending,
            "SENT" => Sent,
            _ => throw new ArgumentException("Invalid Form Status")
        };
    }
}
