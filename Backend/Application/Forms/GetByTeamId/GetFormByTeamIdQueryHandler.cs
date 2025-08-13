using Application.Abstractions;
using Application.Forms.Common;
using Application.Forms.Common.Response;
using Domain.Forms;
using Shared;

namespace Application.Forms.GetByTeamId;

internal sealed class GetFormByTeamIdQueryHandler : IQueryHandler<GetFormByTeamIdQuery, List<FormResponse>>
{
    private readonly IFormRepository _formRepository;

    public GetFormByTeamIdQueryHandler(IFormRepository formRepository)
    {
        _formRepository = formRepository;
    }

    public async Task<Result<List<FormResponse>>> Handle(GetFormByTeamIdQuery request, CancellationToken cancellationToken)
    {
        var forms = await _formRepository.GetByTeamId(request.TeamId);
        return forms.Select(f => f.ToFormResponse()).ToList();
    }
}
