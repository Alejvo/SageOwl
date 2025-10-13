namespace Domain.Qualifications;

public interface IQualificationRepository
{
    Task<IEnumerable<Qualification>> GetQualificationByTeamId(Guid teamId);
    Task<IEnumerable<Qualification>> GetQualificationsByUserId(Guid userId);
    Task<bool> CreateQualifications(Qualification qualification);
    Task<bool> UpdateQualifications(Qualification qualification);
    Task<Qualification?> GetQualificationById(Guid id);
}
