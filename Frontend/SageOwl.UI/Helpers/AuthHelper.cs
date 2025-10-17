using System.IdentityModel.Tokens.Jwt;

namespace SageOwl.UI.Helpers;

public static class AuthHelper
{
    public static bool IsUserAuthenticated(HttpRequest request)
    {
        var token = request.Cookies["AccessToken"];
        if (string.IsNullOrEmpty(token))
            return false;

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var expClaim = jwtToken.Payload.Exp;
            if (expClaim == null)
                return false;

            var expiry = DateTimeOffset.FromUnixTimeSeconds((long)expClaim).UtcDateTime;
            return expiry > DateTime.UtcNow;
        }
        catch
        {
            return false;
        }
    }
}
