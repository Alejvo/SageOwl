using Application.Interfaces;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginRequest request)
    {
        var (accessToken,refreshToken) = await _authService.LoginAsync(request.Email, request.Password);

        return Ok(new
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        try
        {
            var (accessToken, refreshToken) = await _authService.RefreshTokenAsync(request.RefreshToken);

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}
