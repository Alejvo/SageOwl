using Application.Abstractions;
using Application.Forms.Common.Response;

namespace Application.Forms.Queries.GetById;

public record GetFormByIdQuery(Guid FormId) : IQuery<FormResponse>;
