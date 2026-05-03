using System.IdentityModel.Tokens.Jwt;

namespace SageOwl.UI.Models.Users;

public class CurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? Id
    {
        get
        {
            var context = _httpContextAccessor.HttpContext;

            var token = context?.Items["AccessToken"]?.ToString();

            return GetUserIdFromToken(token);
        }
    }

    public Guid? GetUserIdFromToken(string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return null;

        try
        {
            var handler = new JwtSecurityTokenHandler();

            var jwtToken = handler.ReadJwtToken(token);

            var userId =
                jwtToken.Claims
                    .FirstOrDefault(c => c.Type == "sub")
                    ?.Value;

            if (string.IsNullOrWhiteSpace(userId))
                return null;

            return Guid.TryParse(userId, out var id)
                ? id
                : null;
        }
        catch
        {
            return null;
        }
    }
}
