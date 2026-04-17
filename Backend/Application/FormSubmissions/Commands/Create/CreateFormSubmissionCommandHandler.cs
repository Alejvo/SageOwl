using Application.Abstractions;
using Application.Interfaces;
using Domain.FormSubmissions;
using Shared;

namespace Application.FormSubmissions.Commands.Create;

internal sealed class CreateFormSubmissionCommandHandler(
    IFormSubmissionRepository formSubmissionRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateFormSubmissionCommand>
{
    private readonly IFormSubmissionRepository _formSubmissionRepository = formSubmissionRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(CreateFormSubmissionCommand request, CancellationToken cancellationToken)
    {
        var formSubmission = FormSubmission.Create(request.FormId,request.UserId,request.SubmittedAt);

        if (formSubmission is null)
            return Result.Failure(FormSubmissionErrors.FormSubmissionNotFound());

        foreach (var answer in request.Answers)
        {
            formSubmission.AnswerQuestion(answer.QuestionId, answer.Value);
        }

        await _formSubmissionRepository.CreateFormSubmission(formSubmission);

        return await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? Result.Success()
            : Result.Failure(Error.DBFailure);
    }
}
