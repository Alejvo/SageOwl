using Application.Abstractions;
using Application.Forms.Common.Response;

namespace Application.Forms.Queries.GetByTeamId;

public record GetFormByTeamIdQuery(Guid TeamId):IQuery<List<FormResponse>>;
