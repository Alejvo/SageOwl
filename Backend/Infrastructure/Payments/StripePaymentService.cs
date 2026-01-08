using Application.Abstractions;
using Application.Interfaces;
using Stripe;

namespace Infrastructure.Payments;

public class StripePaymentService : IPaymentService
{
    public async Task<PaymentIntentResult> CreatePaymentIntent(
        decimal amount,
        string currency,
        string description)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)(amount * 100),
            Currency = currency,
            Description = description,
            AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
            {
                Enabled = true
            }
        };

        var service = new PaymentIntentService();
        var intent = await service.CreateAsync(options);

        return new PaymentIntentResult
        {
            PaymentIntentId = intent.Id,
            ClientSecret = intent.ClientSecret
        };
    }
}
