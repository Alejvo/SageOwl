using FluentValidation;

namespace Application.Users.Update;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty();

        RuleFor(u => u.Name)
            .NotEmpty();

        RuleFor(u => u.Surname)
            .NotEmpty();

        RuleFor(u =>u.Email)
            .EmailAddress()
            .NotEmpty();

        RuleFor(u =>u.Password)
            .NotEmpty();

        RuleFor(u => u.Username)
            .NotEmpty();

        RuleFor(u => u.Birthday)
            .GreaterThan(new DateTime(1900, 1, 1))
            .LessThan(DateTime.Now.AddDays(1))
            .NotEmpty();
    }
}
