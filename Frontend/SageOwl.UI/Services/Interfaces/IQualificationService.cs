using SageOwl.UI.Models.Qualifications;
using System.Net;

namespace SageOwl.UI.Services.Interfaces;

public interface IQualificationService
{
    Task<Qualification> GetGetQualificationByUserId(Guid userId);
    Task<Qualification> GetQualificationByTeamId(Guid teamId);
    Task<HttpStatusCode> SaveQualifications(SaveQualification qualification);
}
