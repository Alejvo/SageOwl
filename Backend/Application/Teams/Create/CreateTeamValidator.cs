using FluentValidation;

namespace Application.Teams.Create;

public class CreateTeamValidator : AbstractValidator<CreateTeamCommand>
{
    public CreateTeamValidator()
    {
        RuleFor(t => t.Name).NotEmpty();

        RuleFor(t => t.Description).NotEmpty();

        RuleFor(t => t.Members).NotNull();
    }
}
