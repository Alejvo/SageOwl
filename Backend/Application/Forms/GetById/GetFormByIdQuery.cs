using Application.Abstractions;
using Application.Forms.Common.Response;

namespace Application.Forms.GetById;

public record GetFormByIdQuery(Guid FormId) : IQuery<FormResponse>;
