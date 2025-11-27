using FluentValidation;

namespace Application.Qualifications.GetByUserId;

public class GetQualificationsByUserIdValidator : AbstractValidator<GetQualificationsByUserIdQuery>
{
    public GetQualificationsByUserIdValidator()
    {
        RuleFor(q => q.UserId).NotNull();
    }
}
