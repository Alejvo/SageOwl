using Shared;

namespace Domain.Forms;

public class FormErrors
{
    public static Error FormNotFound() => new("Forms.NotFound", $"Form was not found", ErrorType.NotFound);
}
