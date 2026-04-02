using Application.Interfaces;
using Application.Subscriptions.Save;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SageOwl.API.Requests;
using SageOwl.API.Settings;
using Stripe;

namespace SageOwl.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly IMediator _mediator;
    private readonly ILogger<PaymentsController> _logger;
    private readonly StripeSettings _stripeSettings;

    public PaymentsController(IPaymentService paymentService, 
        IMediator mediator,
        ILogger<PaymentsController> logger,
        IOptions<StripeSettings> stripeOptions)
    {
        _paymentService = paymentService;
        _mediator = mediator;
        _logger = logger;
        _stripeSettings = stripeOptions.Value;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePaymentIntent(
        [FromBody] CreatePaymentRequest request)
    {
        if (request.Amount <= 0)
            return BadRequest("Invalid amount");

        var result = await _paymentService.CreatePaymentIntent(
            request.Amount,
            request.Description,
            request.SubscriberId,
            request.PlanId,
            request.SucessUrl
        );

        return Ok(result);
    }
    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                _stripeSettings.WebhookSecret
            );

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    {
                        var intent = stripeEvent.Data.Object as PaymentIntent;
                        if (intent == null) return Ok();

                        if (!intent.Metadata.TryGetValue("userId", out var userIdStr) ||
                            !intent.Metadata.TryGetValue("planId", out var planIdStr))
                            return Ok();

                        if (!Guid.TryParse(userIdStr, out var userId))
                            return Ok();

                        if (!int.TryParse(planIdStr, out var planId))
                            return Ok();

                        var command = new SaveSubscriptionCommand(
                            userId,
                            planId,
                            DateTime.UtcNow,
                            DateTime.UtcNow.AddMonths(1)
                        );

                        await _mediator.Send(command);

                        break;
                    }

                default:
                    break;
            }

            return Ok();
        }
        catch (StripeException ex)
        {
            _logger.LogError(ex, "Stripe signature validation failed");
            return BadRequest();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Stripe webhook processing failed");
            return StatusCode(500);
        }
    }

}
