using SageOwl.UI.Models;
using SageOwl.UI.ViewModel;

namespace SageOwl.UI.Services.Interfaces;

public interface IAccountService
{
    Task<LoginResponse?> Login(LoginViewModel credentials);
}
