using SageOwl.UI.Models;
using SageOwl.UI.ViewModels.Users;
using System.Net;

namespace SageOwl.UI.Services.Interfaces;

public interface IUserService
{
    Task<HttpStatusCode> Create(RegisterViewModel register);
    Task<HttpStatusCode> Update(UpdateUserViewModel user);
    Task<User?> GetUserFromToken(string token);
    Task<List<User>> GetUsers(int Page, int PageSize,string? SearchTerm,string? SortColumn,string? SortOrder);
    Task<HttpStatusCode> DeleteUser(Guid userId);
}
