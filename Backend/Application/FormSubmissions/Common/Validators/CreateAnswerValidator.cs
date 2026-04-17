using Application.FormSubmissions.Common.Request;
using FluentValidation;

namespace Application.FormSubmissions.Common.Validators;

public class CreateAnswerValidator : AbstractValidator<CreateAnswerRequest>
{
    public CreateAnswerValidator()
    {
        RuleFor(a => a.Value).NotNull();

        RuleFor(a => a.QuestionId).NotNull();
    }
}
