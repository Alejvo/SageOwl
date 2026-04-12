namespace Domain.Qualifications;

public record UserQualificationInput(
    Guid UserId,
    double Grade,
    string Description
    );
