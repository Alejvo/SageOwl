using Application.Abstractions;
using Application.Interfaces;
using Stripe;
using Stripe.Checkout;

namespace Infrastructure.Payments;

public class StripePaymentService : IPaymentService
{
    public async Task<CheckoutSessionResult> CreatePaymentIntent(
        decimal amount,
        string description,
        Guid subscriberId,
        int planId,
        string successUrl)
    {
        var options = new SessionCreateOptions
        {
            SuccessUrl = successUrl,
            Mode = "payment",
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new()
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = (long)(amount * 100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = description
                        }
                    },
                    Quantity = 1
                }
            },
            PaymentIntentData = new SessionPaymentIntentDataOptions
            {
                Metadata = new Dictionary<string, string>
                {
                    ["userId"] = subscriberId.ToString(),
                    ["planId"] = planId.ToString()
                }
            }
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return new CheckoutSessionResult
        {
            SessionId = session.Id,
            CheckoutUrl = session.Url
        };
    }
}
