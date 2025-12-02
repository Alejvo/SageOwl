using Application.Forms.Common.Request;
using FluentValidation;

namespace Application.Forms.Common.Validators;

public class OptionRequestValidator : AbstractValidator<CreateOptionRequest>
{
    public OptionRequestValidator()
    {
        RuleFor(o => o.Value).NotEmpty();
    }
}
