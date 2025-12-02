using Domain.Users;
using FluentValidation;

namespace Application.Users.Commands.Create;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator(IUserRepository userRepository)
    {
        RuleFor(u => u.Name)
            .NotEmpty();

        RuleFor(u => u.Surname)
            .NotEmpty();

        RuleFor(u => u.Email)
            .EmailAddress()
            .MustAsync((email, cancellationToken) => userRepository.EmailExists(email))
            .NotEmpty();

        RuleFor(u => u.Password)
            .NotEmpty();

        RuleFor(u => u.Username)
            .NotEmpty();

        RuleFor(u => u.Birthday)
            .GreaterThan(new DateTime(1900, 1, 1))
            .LessThan(DateTime.Now.AddDays(1))
            .NotEmpty();

    }
}
