namespace Application.Qualifications.Common;

public record UserQualificationDto(
    Guid UserId, 
    double Grade, 
    string Description
    );
