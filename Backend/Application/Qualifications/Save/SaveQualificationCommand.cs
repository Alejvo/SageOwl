using Application.Abstractions;
using Application.Qualifications.Common;

namespace Application.Qualifications.Save;

public record SaveQualificationCommand(
    Guid TeamId, 
    double MinimumGrade, 
    double MaximumGrade, 
    double PassingGrade, 
    string Period,
    int TotalGrades,
    List<UserQualificationDto> UserQualifications
    ) :ICommand;
