namespace SageOwl.UI.Services.Interfaces;

public interface ITokenProvider
{
    Task<string?> GetAccessTokenAsync();
    bool TokenExpired(string? token);
}
