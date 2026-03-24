using FluentValidation;

namespace Application.Teams.GetById;

public class GetTeamByIdValidator : AbstractValidator<GetTeamByIdQuery>
{
    public GetTeamByIdValidator()
    {
        RuleFor(t => t.TeamId).NotEmpty();
    }
}
