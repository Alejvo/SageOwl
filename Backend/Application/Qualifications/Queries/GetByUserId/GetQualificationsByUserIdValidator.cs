using FluentValidation;

namespace Application.Qualifications.Queries.GetByUserId;

public class GetQualificationsByUserIdValidator : AbstractValidator<GetQualificationsByUserIdQuery>
{
    public GetQualificationsByUserIdValidator()
    {
        RuleFor(q => q.UserId).NotNull();
    }
}
