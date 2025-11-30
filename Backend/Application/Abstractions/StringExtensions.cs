namespace Application.Abstractions;

public static class StringExtensions
{
    public static string Capitalize(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;

        value = value.Trim().ToLower();
        return char.ToUpper(value[0]) + value.Substring(1);
    }
}
