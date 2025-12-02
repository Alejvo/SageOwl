using Application.Abstractions;
using Application.Forms.Common;
using Application.Forms.Common.Response;
using Domain.Forms;
using Shared;

namespace Application.Forms.Queries.GetById;

internal sealed class GetFormByIdQueryHandler : IQueryHandler<GetFormByIdQuery, FormResponse>
{
    private readonly IFormRepository _formRepository;

    public GetFormByIdQueryHandler(IFormRepository formRepository)
    {
        _formRepository = formRepository;
    }

    public async Task<Result<FormResponse>> Handle(GetFormByIdQuery request, CancellationToken cancellationToken)
    {
        var form = await _formRepository.GetFormById(request.FormId);

        return form == null 
            ? Result.Failure<FormResponse>(Error.DBFailure) 
            : form.ToFormResponse();
    }
}
