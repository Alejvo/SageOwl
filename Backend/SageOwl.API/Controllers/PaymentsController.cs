using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SageOwl.API.Requests;

namespace SageOwl.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("create-intent")]
    public async Task<IActionResult> CreatePaymentIntent(
        [FromBody] CreatePaymentRequest request)
    {
        if (request.Amount <= 0)
            return BadRequest("Invalid amount");

        var result = await _paymentService.CreatePaymentIntent(
            request.Amount,
            request.Currency,
            request.Description
        );

        return Ok(result);
    }
}
