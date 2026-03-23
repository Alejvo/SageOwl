using SageOwl.UI.Models;
using SageOwl.UI.ViewModels.UI;

namespace SageOwl.UI.Services.Interfaces;

public interface IAccountService
{
    Task<LoginResponse?> Login(LoginViewModel credentials);
    Task<string?> GetValidAccessTokenAsync();

}
