using Application.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SageOwl.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email,string password)
    {
        var (accessToken,refreshToken) = await _authService.LoginAsync(email, password);

        return Ok(new
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }
}
