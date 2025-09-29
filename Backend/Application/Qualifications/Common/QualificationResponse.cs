namespace Application.Qualifications.Common;

public record QualificationResponse(
    Guid Id, 
    Guid TeamId, 
    double MinimumGrade, 
    double MaximumGrade,
    double PassingGrade,
    string Period,
    int TotalGrades,
    IEnumerable<UserQualificationResponse> UserQualifications
    );
