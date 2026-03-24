using Application.Forms.Common.Request;
using FluentValidation;

namespace Application.Forms.Common.Validators;

public class CreateQuestionValidator : AbstractValidator<CreateQuestionRequest>
{
    public CreateQuestionValidator()
    {
        RuleFor(f => f.Title).NotEmpty();
        RuleFor(f => f.QuestionType).NotEmpty();

        RuleForEach(q => q.Options)
            .SetValidator(new CreateOptionValidator());
    }
}
