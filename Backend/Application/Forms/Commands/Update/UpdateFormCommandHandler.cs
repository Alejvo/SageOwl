using Application.Abstractions;
using Application.Interfaces;
using Domain.Forms;
using Shared;

namespace Application.Forms.Commands.Update;

internal sealed class UpdateFormCommandHandler : ICommandHandler<UpdateFormCommand>
{
    private readonly IFormRepository _formRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFormCommandHandler(IFormRepository formRepository, IUnitOfWork unitOfWork)
    {
        _formRepository = formRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateFormCommand request, CancellationToken cancellationToken)
    {
        var form = await _formRepository.GetFormById(request.FormId);
        if (form == null)
            return Result.Failure(FormErrors.FormNotFound());

        //form.Update(request.Title,request.Deadline);

        /*
        foreach (var qReq in request.Questions)
        {
            if (qReq.QuestionId is null && !qReq.IsDeleted)
            {
                // Add Question
                form.AddQuestion(qReq);
                continue;
            }

            if (qReq.QuestionId is not null && !qReq.IsDeleted)
            {
                // Update Question
                var question = form.UpdateQuestion(
                    qReq.QuestionId.Value,
                    qReq.Title,
                    qReq.Description
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
        }*/

        return await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? Result.Success()
            : Result.Failure(Error.DBFailure);
    }
}
