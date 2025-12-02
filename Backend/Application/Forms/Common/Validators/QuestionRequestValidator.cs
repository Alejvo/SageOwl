using Application.Forms.Common.Request;
using FluentValidation;

namespace Application.Forms.Common.Validators;

public class QuestionRequestValidator : AbstractValidator<CreateQuestionRequest>
{
    public QuestionRequestValidator()
    {
        RuleFor(f => f.Title).NotEmpty();
        RuleFor(f => f.QuestionType).NotEmpty();

        RuleForEach(q => q.Options)
            .SetValidator(new OptionRequestValidator());
    }
}
