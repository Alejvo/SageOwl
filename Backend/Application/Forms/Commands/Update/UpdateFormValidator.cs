using FluentValidation;

namespace Application.Forms.Commands.Update;

public class UpdateFormValidator : AbstractValidator<UpdateFormCommand>
{
    public UpdateFormValidator()
    {
        RuleFor(f => f.FormId).NotEmpty();
        RuleFor(f => f.Title).NotEmpty();
        RuleFor(f => f.TeamId).NotEmpty();
        RuleFor(f => f.Deadline).NotEmpty();
    }
}
