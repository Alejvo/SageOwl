using FluentValidation;

namespace Application.Users.GetById;

public class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty();
    }
}
