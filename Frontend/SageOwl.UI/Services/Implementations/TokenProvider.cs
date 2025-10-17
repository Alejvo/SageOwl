using SageOwl.UI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace SageOwl.UI.Services.Implementations;

public class TokenProvider : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IAccountService _accountService;

    public TokenProvider(IHttpContextAccessor contextAccessor, IAccountService accountService)
    {
        _contextAccessor = contextAccessor;
        _accountService = accountService;
    }

    public async Task<string?> GetAccessTokenAsync()
    {
        var context = _contextAccessor.HttpContext;
        if (context == null) return null;

        var token = context.Request.Cookies["AccessToken"];
        var refreshToken = context.Request.Cookies["RefreshToken"];

        if (string.IsNullOrEmpty(token) || TokenExpired(token))
        {
            if (string.IsNullOrEmpty(refreshToken)) return null;
            token = await _accountService.GetValidAccessTokenAsync();
        }

        return token;
    }
    public bool TokenExpired(string? token)
    {
        if (string.IsNullOrEmpty(token)) return true;
        try
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return jwt.ValidTo <= DateTime.UtcNow;
        }
        catch
        {
            return true;
        }
    }
}
