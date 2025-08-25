using SageOwl.UI.Models;

namespace SageOwl.UI.Services.Interfaces;

public interface ITeamService
{
    Task<List<Team>> GetTeamsByUserToken(string token);
}
