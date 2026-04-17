using Shared;

namespace Domain.Qualifications;

public class QualificationError
{
    public static Error QualificationNotFound(Guid qualificationId) => new("Qualifications.NotFound",$"Qualification with id {qualificationId} was not found", ErrorType.NotFound);
}
