using FluentValidation;

namespace Application.Qualifications.Save;

public class SaveQualificationValidator : AbstractValidator<SaveQualificationCommand>
{
    public SaveQualificationValidator()
    {
        RuleFor(q => q.TeamId).NotNull();

        RuleFor(q => q.MinimumGrade).GreaterThanOrEqualTo(0);

        RuleFor(q => q.MaximumGrade).GreaterThanOrEqualTo(0);

        RuleFor(q => q.PassingGrade).GreaterThanOrEqualTo(0);

        RuleFor(q => q.Period).NotEmpty();

        RuleFor(q => q.TotalGrades).NotNull();
    }
}
