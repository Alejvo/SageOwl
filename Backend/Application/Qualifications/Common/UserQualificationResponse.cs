namespace Application.Qualifications.Common;

public record UserQualificationResponse(
    Guid Id, 
    Guid UserId,
    string Name,
    Guid QualificationId,
    double Grade, 
    string? Description, 
    int Position, 
    bool HasValue
    );
