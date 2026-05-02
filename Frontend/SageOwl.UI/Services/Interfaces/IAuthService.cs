using SageOwl.UI.Models.Tokens;
using SageOwl.UI.ViewModels.UI;

namespace SageOwl.UI.Services.Interfaces;

public interface IAuthService
{
    Task<string?> GetAccessTokenAsync();
    bool IsTokenExpired(string token);
    Task<Token> RefreshToken(string refreshToken);
    Task<Token> Login(LoginViewModel viewModel);
}
