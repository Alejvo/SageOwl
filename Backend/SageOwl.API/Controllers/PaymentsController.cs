using Application.Interfaces;
using Application.Subscriptions.Save;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SageOwl.API.Requests;
using Stripe;

namespace SageOwl.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;

    public PaymentsController(IPaymentService paymentService, IConfiguration configuration,IMediator mediator)
    {
        _paymentService = paymentService;
        _configuration = configuration;
        _mediator = mediator;
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
            Event? stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], _configuration["StripeWebhookSecret"]);
            var intent = stripeEvent.Data.Object as PaymentIntent;

            var userId = Guid.Parse(intent.Metadata["userId"]);
            var planId = Guid.Parse(intent.Metadata["planId"]);
            // Handle the event
            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    var command = new SaveSubscriptionCommand 
                    (
                        userId,
                       planId,
                        DateTime.UtcNow,
                        DateTime.UtcNow.AddMonths(1)
                    );
                    var result = await _mediator.Send(command);
                    return Ok(result);

                default:
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                    return BadRequest();
            }
        }
        catch (StripeException e)
        {
            return BadRequest();
        }
    }

}
