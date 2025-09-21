namespace Domain.Qualifications;

public interface IQualificationRepository
{
    Task<IEnumerable<Qualification>> GetQualificationsByTeamId(Guid teamId);
    Task<IEnumerable<Qualification>> GetQualificationsByUserId(Guid userId);
    Task<bool> SaveQualifications(Qualification qualification);
}
