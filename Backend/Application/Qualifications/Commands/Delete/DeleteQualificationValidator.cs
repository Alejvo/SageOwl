using FluentValidation;

namespace Application.Qualifications.Commands.Delete;

public class DeleteQualificationValidator : AbstractValidator<DeleteQualificationCommand>
{
    public DeleteQualificationValidator()
    {
        RuleFor(q => q.Id).NotNull();
    }
}
