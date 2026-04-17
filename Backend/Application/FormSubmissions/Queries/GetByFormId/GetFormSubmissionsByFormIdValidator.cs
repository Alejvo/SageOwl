using FluentValidation;

namespace Application.FormSubmissions.Queries.GetByFormId;

public class GetFormSubmissionsByFormIdValidator : AbstractValidator<GetFormSubmissionsByFormIdQuery>
{
    public GetFormSubmissionsByFormIdValidator()
    {
        RuleFor(f => f.FormId);
    }
}
