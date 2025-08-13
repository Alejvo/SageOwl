using Application.Abstractions;
using Application.Forms.Common.Response;

namespace Application.Forms.GetByTeamId;

public record GetFormByTeamIdQuery(Guid TeamId):IQuery<List<FormResponse>>;
