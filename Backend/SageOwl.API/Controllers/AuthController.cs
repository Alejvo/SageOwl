using Application.Auth.Login;
using Application.Auth.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace SageOwl.API.Controllers;

[Route("api/[controller]")]
public class AuthController : ApiController
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginRequest request)
    {
        var res = await _sender.Send(new LoginCommand(request.Email, request.Password));

        return res.IsSuccess ? Ok(res.Value): Problem(res.Errors);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        var res = await _sender.Send(new RefreshTokenCommand(request.RefreshToken));

        return res.IsSuccess ? Ok(res.Value) : Problem(res.Errors);
    }
}
