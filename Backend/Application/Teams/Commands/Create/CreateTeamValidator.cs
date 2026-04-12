using FluentValidation;

namespace Application.Teams.Commands.Create;

public class CreateTeamValidator : AbstractValidator<CreateTeamCommand>
{
    public CreateTeamValidator()
    {
        RuleFor(t => t.Name).NotEmpty();

        RuleFor(t => t.Description).NotEmpty();

        RuleFor(t => t.Members).NotNull();
    }
}
