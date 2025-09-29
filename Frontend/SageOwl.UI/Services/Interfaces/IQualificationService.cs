using SageOwl.UI.Models;
using System.Net;

namespace SageOwl.UI.Services.Interfaces;

public interface IQualificationService
{
    Task<List<Qualification>> GetGetQualificationsByUserId(Guid userId);
    Task<List<Qualification>> GetQualificationsByTeamId(Guid teamId);
    Task<HttpStatusCode> SaveQualifications(Qualification qualification);
}
