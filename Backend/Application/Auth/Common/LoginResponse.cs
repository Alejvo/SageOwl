namespace Application.Auth.Common;

public class LoginResponse
{
    string AccessToken { get; } = string.Empty;
    string RefreshToken { get; } = string.Empty;

    public LoginResponse(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}
