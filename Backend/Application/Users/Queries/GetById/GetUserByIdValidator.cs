using FluentValidation;

namespace Application.Users.Queries.GetById;

public class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty();
    }
}
