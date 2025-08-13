using Application.Abstractions;
using Domain.Forms;
using Shared;

namespace Application.Forms.Create;

internal sealed class CreateFormCommandHandler : ICommandHandler<CreateFormCommand>
{
    private readonly IFormRepository _formRepository;

    public CreateFormCommandHandler(IFormRepository formRepository)
    {
        _formRepository = formRepository;
    }

    public async Task<Result> Handle(CreateFormCommand request, CancellationToken cancellationToken)
    {
        var form = Form.Create(request.TeamId, request.Title, request.Deadline);

        foreach (var question in request.Questions)
        {
            var newQuestion = form.AddQuestion(question.Title, question.Description, form.Id,QuestionType.FromString(question.QuestionType));

            foreach (var option in question.Options)
            {
                newQuestion.AddOption(option.Value,option.IsCorrect);
            }
        }

        return await _formRepository.CreateForm(form)
            ? Result.Success()
            : Result.Failure(Error.DBFailure);
    }

       
}
