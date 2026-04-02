namespace Domain.Subscriptions;

public class Plan
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Value { get; set; }
    public Plan() { }

    private Plan(string name, double value)
    {
        Name = name;
        Value = value;
    }

    public static Plan Create(string name, double value)
        => new (name, value);

}