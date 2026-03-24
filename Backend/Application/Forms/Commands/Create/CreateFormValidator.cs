using Application.Forms.Common.Validators;
using FluentValidation;

namespace Application.Forms.Commands.Create;

public class CreateFormValidator : AbstractValidator<CreateFormCommand>
{
    public CreateFormValidator()
    {
        RuleFor(f => f.Title)
            .NotEmpty();

        RuleFor(f => f.TeamId)
            .NotEmpty();

        RuleFor(f => f.Deadline)
            .NotEmpty();

        RuleForEach(x => x.Questions)
            .SetValidator(new CreateQuestionValidator());
    }
}
