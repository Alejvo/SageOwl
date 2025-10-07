using Application.Abstractions;
using Application.Qualifications.Common;

namespace Application.Qualifications.Update;

public record UpdateQualificationCommand(
    Guid Id,
    Guid TeamId,
    double MinimumGrade,
    double MaximumGrade,
    double PassingGrade,
    string Period,
    int TotalGrades,
    List<UserQualificationDto> UserQualifications
    ):ICommand;
