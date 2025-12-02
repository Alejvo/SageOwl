using Application.Forms.Common.Request;
using FluentValidation;

namespace Application.Forms.Common.Validators;

public class UpdateOptionValidator : AbstractValidator<UpdateOptionRequest>
{
    public UpdateOptionValidator()
    {
        RuleFor(o => o.Value).NotEmpty();
    }
}
