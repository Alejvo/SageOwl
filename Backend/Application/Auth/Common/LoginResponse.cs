namespace Application.Auth.Common;

public record LoginResponse(
    string AccessToken,
    string RefreshToken
);
