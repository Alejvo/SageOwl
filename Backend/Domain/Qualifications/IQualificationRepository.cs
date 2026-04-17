namespace Domain.Qualifications;

public interface IQualificationRepository
{
    Task<IEnumerable<Qualification>> GetQualificationByTeamId(Guid teamId);
    Task<IEnumerable<Qualification>> GetQualificationsByUserId(Guid userId);
    Task CreateQualification(Qualification qualification);
    Task<Qualification?> GetQualificationById(Guid id);
    void DeleteQualification(Qualification qualification);
}
