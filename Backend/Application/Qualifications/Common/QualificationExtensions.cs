using Domain.Qualifications;

namespace Application.Qualifications.Common;

public static class QualificationExtensions
{
    public static QualificationResponse ToResponse(this Qualification qualification)
    {
        return new QualificationResponse
        (
            qualification.Id,
            qualification.TeamId,
            qualification.MinimumGrade,
            qualification.MaximumGrade,
            qualification.PassingGrade,
            qualification.Period,
            qualification.UsersQualifications.Select(q => new UserQualificationResponse
            (
                q.Id,
                q.UserId,
                q.Grade,
                q.Description,
                q.Position,
                q.HasValue
            ))
        );
    }
}
