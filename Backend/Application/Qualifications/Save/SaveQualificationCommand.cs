using Application.Abstractions;
using Application.Qualifications.Common;

namespace Application.Qualifications.Save;

public record SaveQualificationCommand(
    Guid TeamId, 
    double MinimumGrade, 
    double MaximumGrade, 
    double PassingGrade, 
    int Period,
    List<UserQualificationDto> UserQualifications
    ) :ICommand;
