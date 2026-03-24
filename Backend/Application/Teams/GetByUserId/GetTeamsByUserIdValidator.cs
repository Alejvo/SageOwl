using FluentValidation;

namespace Application.Teams.GetByUserId;

public class GetTeamsByUserIdValidator : AbstractValidator<GetTeamsByUserIdQuery>
{
    public GetTeamsByUserIdValidator()
    {
        RuleFor(t => t.UserId).NotEmpty();
    }
}
