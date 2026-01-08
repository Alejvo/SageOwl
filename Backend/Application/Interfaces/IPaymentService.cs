using Application.Abstractions;

namespace Application.Interfaces;

public interface IPaymentService
{
    Task<PaymentIntentResult> CreatePaymentIntent(
        decimal amount,
        string currency,
        string description);
}
