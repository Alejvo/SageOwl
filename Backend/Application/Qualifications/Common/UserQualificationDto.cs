namespace Application.Qualifications.Common;

public record UserQualificationDto(
    Guid UserId, 
    double Grade, 
    int Position, 
    bool HasValue, 
    string? Description = null
    );
