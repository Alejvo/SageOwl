using Application.Forms.Common.Validators;
using Application.FormSubmissions.Common.Validators;
using FluentValidation;

namespace Application.FormSubmissions.Commands.Create;

public class CreateFormSubmissionValidator : AbstractValidator<CreateFormSubmissionCommand>
{
    public CreateFormSubmissionValidator()
    {
        RuleFor(f => f.SubmittedAt).NotNull();

        RuleFor(f => f.UserId).NotNull();

        RuleFor(f => f.FormId).NotNull();

        RuleForEach(x => x.Answers)
            .SetValidator(new CreateAnswerValidator());
    }
}
