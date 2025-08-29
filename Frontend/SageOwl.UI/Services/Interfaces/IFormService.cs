using SageOwl.UI.Models;

namespace SageOwl.UI.Services.Interfaces;

public interface IFormService
{
    Task<List<Form>> GetFormsByUserId();
}
