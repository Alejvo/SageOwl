using Microsoft.AspNetCore.Identity.Data;
using SageOwl.UI.Models;
using SageOwl.UI.ViewModel;

namespace SageOwl.UI.Services;

public interface IAccountService
{
    Task<LoginResponse?> Login(LoginViewModel credentials);
}
