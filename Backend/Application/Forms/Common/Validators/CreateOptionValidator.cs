using Application.Forms.Common.Request;
using FluentValidation;

namespace Application.Forms.Common.Validators;

public class CreateOptionValidator : AbstractValidator<CreateOptionRequest>
{
    public CreateOptionValidator()
    {
        RuleFor(o => o.Value).NotEmpty();
    }
}
