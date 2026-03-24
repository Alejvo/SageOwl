using Application.Forms.Common.Request;
using FluentValidation;

namespace Application.Forms.Common.Validators;

public class UpdateQuestionValidator : AbstractValidator<UpdateQuestionRequest>
{
    public UpdateQuestionValidator()
    {
        RuleFor(f => f.Title).NotEmpty();
        RuleFor(f => f.QuestionType).NotEmpty();
        RuleForEach(q => q.Options)
            .SetValidator(new UpdateOptionValidator());
    }
}
