namespace Application.Abstractions;

public class PaymentIntentResult
{
    public string PaymentIntentId { get; init; }
    public string ClientSecret { get; init; }
}
