using FluentValidation;

namespace Application.Forms.Queries.GetById;

public class GetFormByIdValidator : AbstractValidator<GetFormByIdQuery>
{
    public GetFormByIdValidator()
    {
        RuleFor(f => f.FormId).NotEmpty();
    }
}
