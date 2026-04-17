using Application.Abstractions;
using Application.FormSubmissions.Common;
using Application.FormSubmissions.Common.Response;
using Application.Interfaces;
using Domain.FormSubmissions;
using Shared;

namespace Application.FormSubmissions.Queries.GetByFormId;

internal sealed class GetFormSubmissionsByFormIdQueryHandler : IQueryHandler<GetFormSubmissionsByFormIdQuery, IEnumerable<FormSubmissionResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFormSubmissionRepository _formSubmissionRepository;

    public GetFormSubmissionsByFormIdQueryHandler(IUnitOfWork unitOfWork, IFormSubmissionRepository formSubmissionRepository)
    {
        _unitOfWork = unitOfWork;
        _formSubmissionRepository = formSubmissionRepository;
    }

    public async Task<Result<IEnumerable<FormSubmissionResponse>>> Handle(GetFormSubmissionsByFormIdQuery request, CancellationToken cancellationToken)
    {
        var formSubmissions = await _formSubmissionRepository.GetFormSubmissionsByForm(request.FormId);

        if (formSubmissions is null)
            return Result.Failure<IEnumerable<FormSubmissionResponse>>(FormSubmissionErrors.FormSubmissionNotFound());

        return Result.Success(formSubmissions.Select(f => f.ToResponse()));
    }
}
