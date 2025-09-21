namespace Application.Qualifications.Common;

public record UserQualificationResponse(
    Guid Id, 
    Guid UserId,
    double Grade, 
    string? Description, 
    int Position, 
    bool HasValue
    );
