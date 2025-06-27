using SageOwl.UI.ViewModel;

namespace SageOwl.UI.Services;

public interface IUserService
{
    Task<bool> Create(RegisterViewModel register);
}
