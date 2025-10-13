using SageOwl.UI.Models.Qualifications;
using System.Net;

namespace SageOwl.UI.Services.Interfaces;

public interface IQualificationService
{
    Task<List<Qualification>> GetQualificationByUserId(Guid userId);
    Task<List<Qualification>> GetQualificationByTeamId(Guid teamId);
    Task<HttpStatusCode> SaveQualifications(SaveQualification qualification);
}
