using FluentValidation;

namespace Application.Teams.Commands.Update;

public class UpdateTeamValidator : AbstractValidator<UpdateTeamCommand>
{
    public UpdateTeamValidator()
    {
        RuleFor(t => t.TeamId).NotEmpty();

        RuleFor(t => t.Name).NotEmpty();

        RuleFor(t => t.Description).NotEmpty();

        RuleFor(t => t.Members).NotNull();
    }
}
