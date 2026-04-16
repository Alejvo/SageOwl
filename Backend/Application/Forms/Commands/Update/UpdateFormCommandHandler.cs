using Application.Abstractions;
using Application.Interfaces;
using Domain.Forms;
using Domain.Forms.Dtos;
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


        form.Update(request.Title,request.Deadline);

        form.SyncQuestions(
            request.Questions.Select(x =>
                new QuestionInput(
                    x.Text,
                    x.QuestionType,
                    x.Options?.Select(o =>
                        new OptionInput(
                            o.Value,
                            o.IsCorrect,
                            o.OptionId
                            )),
                    x.QuestionId
                    )
                ).ToList() ?? []
            );

        return await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? Result.Success()
            : Result.Failure(Error.DBFailure);
    }
}
