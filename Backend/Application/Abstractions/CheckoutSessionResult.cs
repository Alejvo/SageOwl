namespace Application.Abstractions;

public class CheckoutSessionResult
{
    public string SessionId { get; init; }
    public string ClientSecret { get; init; }
}
