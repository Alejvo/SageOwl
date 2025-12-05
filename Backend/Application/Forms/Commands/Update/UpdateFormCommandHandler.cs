using Application.Abstractions;
using Domain.Forms;
using Shared;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace Application.Forms.Commands.Update;

internal sealed class UpdateFormCommandHandler : ICommandHandler<UpdateFormCommand>
{
    private readonly IFormRepository _formRepository;

    public UpdateFormCommandHandler(IFormRepository formRepository)
    {
        _formRepository = formRepository;
    }

    public async Task<Result> Handle(UpdateFormCommand request, CancellationToken cancellationToken)
    {
        var form = await _formRepository.GetFormById(request.FormId);
        if (form == null)
            return Result.Failure(FormErrors.FormNotFound());

        form.Update(request.Title,request.Deadline);

        foreach (var qReq in request.Questions)
        {
            if (qReq.QuestionId is null && !qReq.IsDeleted)
            {
                // Add Question
                form.AddQuestion(qReq.Title, qReq.Description,form.Id, QuestionType.FromString(qReq.QuestionType));
                continue;
            }

            if (qReq.QuestionId is not null && !qReq.IsDeleted)
            {
                // Update Question
                var question = form.UpdateQuestion(
                    qReq.QuestionId.Value,
                    qReq.Title,
                    qReq.Description,
                    QuestionType.FromString(qReq.QuestionType)
                );

                foreach (var optionReq in qReq.Options)
                {
                    //Add Option
                    if (optionReq.OptionId is null && !optionReq.IsDeleted)
                    {
                        question.AddOption(optionReq.Value,optionReq.IsCorrect);
                        continue;
                    }

                    //Update Option
                    if (optionReq.OptionId is not null && !optionReq.IsDeleted)
                    {
                        question.UpdateOption(optionReq.OptionId.Value,optionReq.Value, optionReq.IsCorrect);
                        continue;
                    }

                    // Remove Option
                    if (optionReq.OptionId is not null && optionReq.IsDeleted)
                    {
                        question.RemoveOption(optionReq.OptionId.Value);
                    }
                }
            }

            if (qReq.QuestionId is not null && qReq.IsDeleted)
            {
                // Delete Question
                form.RemoveQuestion(qReq.QuestionId.Value);
            }
        }

        return await _formRepository.SaveChanges() 
            ? Result.Success()
            : Result.Failure(Error.DBFailure);
    }
}
