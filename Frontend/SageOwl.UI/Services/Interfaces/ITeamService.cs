using SageOwl.UI.Models;
using SageOwl.UI.ViewModels.Teams;

namespace SageOwl.UI.Services.Interfaces;

public interface ITeamService
{
    Task<List<Team>> GetTeamsByUserToken(string token);
    Task<Team> GetTeamById(Guid teamId);
    Task<bool> CreateTeam(CreateTeamViewModel newTeam);
}
