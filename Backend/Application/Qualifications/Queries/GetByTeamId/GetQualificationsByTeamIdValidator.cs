using FluentValidation;

namespace Application.Qualifications.Queries.GetByTeamId;

public class GetQualificationsByTeamIdValidator : AbstractValidator<GetQualificationsByTeamIdQuery>
{
    public GetQualificationsByTeamIdValidator()
    {
        RuleFor(q => q.TeamId).NotNull();
    }
}
