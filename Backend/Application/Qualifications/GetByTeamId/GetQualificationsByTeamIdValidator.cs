using FluentValidation;

namespace Application.Qualifications.GetByTeamId;

public class GetQualificationsByTeamIdValidator : AbstractValidator<GetQualificationsByTeamIdQuery>
{
    public GetQualificationsByTeamIdValidator()
    {
        RuleFor(q => q.TeamId).NotNull();
    }
}
