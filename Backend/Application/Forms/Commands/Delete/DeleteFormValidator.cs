using FluentValidation;

namespace Application.Forms.Commands.Delete;
public class DeleteFormValidator : AbstractValidator<DeleteFormCommand>
{
    public DeleteFormValidator()
    {
        RuleFor(f => f.FormId).NotEmpty();
    }
}
