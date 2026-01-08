namespace SageOwl.API.Requests;

public class CreatePaymentRequest
{
    public decimal Amount { get; init; }
    public string Currency { get; init; } = "usd";
    public string Description { get; init; }
}
