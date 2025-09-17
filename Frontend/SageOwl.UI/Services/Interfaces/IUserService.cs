using SageOwl.UI.Models;
using SageOwl.UI.ViewModels;

namespace SageOwl.UI.Services.Interfaces;

public interface IUserService
{
    Task<bool> Create(RegisterViewModel register);
    Task<User?> GetUserFromToken(string token);
    Task<List<User>> GetUsers(int Page, int PageSize,string? SearchTerm,string? SortColumn,string? SortOrder);
}
