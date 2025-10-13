namespace Application.Qualifications.Common;

public record QualificationResponse(
    Guid Id, 
    Guid TeamId, 
    string TeamName,
    double MinimumGrade, 
    double MaximumGrade,
    double PassingGrade,
    string Period,
    int TotalGrades,
    IEnumerable<UserQualificationResponse> UserQualifications
    );
