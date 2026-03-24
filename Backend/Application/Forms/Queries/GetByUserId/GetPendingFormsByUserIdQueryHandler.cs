using Application.Abstractions;
using Application.Forms.Common;
using Application.Forms.Common.Response;
using Domain.Forms;
using Shared;

namespace Application.Forms.Queries.GetByUserId;

internal sealed class GetPendingFormsByUserIdQueryHandler : IQueryHandler<GetPendingFormsByUserIdQuery, List<FormResponse>>
{
    private readonly IFormRepository _formRepository;

    public GetPendingFormsByUserIdQueryHandler(IFormRepository formRepository)
    {
        _formRepository = formRepository;
    }

    public async Task<Result<List<FormResponse>>> Handle(GetPendingFormsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var forms = await _formRepository.GetPendingFormsByUserId(request.UserId);
        return forms.Select(f => f.ToFormResponse()).ToList();
    }
}
