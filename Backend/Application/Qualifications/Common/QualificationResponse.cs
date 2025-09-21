namespace Application.Qualifications.Common;

public record QualificationResponse(
    Guid Id, 
    Guid TeamId, 
    double MinimumGrade, 
    double MaximumGrade,
    double PassingGrade,
    int Period,
    IEnumerable<UserQualificationResponse> UserQualification
    );
