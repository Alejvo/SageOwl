using FluentValidation;

namespace Application.Teams.Queries.IsAdmin;

public class IsUserAdminValidator : AbstractValidator<IsUserAdminQuery>
{
    public IsUserAdminValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();

        RuleFor(x => x.TeamId).NotEmpty();
    }
}
