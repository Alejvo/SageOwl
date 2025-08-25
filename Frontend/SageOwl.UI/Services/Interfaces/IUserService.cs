using SageOwl.UI.Models;
using SageOwl.UI.ViewModel;

namespace SageOwl.UI.Services.Interfaces;

public interface IUserService
{
    Task<bool> Create(RegisterViewModel register);
    Task<User?> GetUserFromToken(string token);
}
