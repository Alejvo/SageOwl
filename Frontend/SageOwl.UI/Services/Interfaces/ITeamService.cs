using SageOwl.UI.Models;
using SageOwl.UI.Models.Teams;
using SageOwl.UI.ViewModels.Teams;
using System.Net;

namespace SageOwl.UI.Services.Interfaces;

public interface ITeamService
{
    Task<List<Team>> GetTeamsByUser();
    Task<Team> GetTeamById(Guid teamId);
    Task<HttpStatusCode> CreateTeam(CreateTeamViewModel newTeam);
    Task<HttpStatusCode> UpdateTeam(UpdateTeamDto updateTeam);
    Task<HttpStatusCode> DeleteTeam(Guid teamId);
}
