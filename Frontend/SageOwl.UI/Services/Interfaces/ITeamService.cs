using SageOwl.UI.Models;
using SageOwl.UI.ViewModels.Teams;

namespace SageOwl.UI.Services.Interfaces;

public interface ITeamService
{
    Task<List<Team>> GetTeamsByUser();
    Task<Team> GetTeamById(Guid teamId);
    Task<bool> CreateTeam(CreateTeamViewModel newTeam);
    Task<bool> UpdateTeam(UpdateTeamDto updateTeam);
}
