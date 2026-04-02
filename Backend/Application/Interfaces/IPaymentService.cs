using Application.Abstractions;

namespace Application.Interfaces;

public interface IPaymentService
{
    Task<CheckoutSessionResult> CreatePaymentIntent(
        decimal amount,
        string description,
        Guid subscriberId,
        int planId,
        string successUrl);
}
