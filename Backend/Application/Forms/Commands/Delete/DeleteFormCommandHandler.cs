using Application.Abstractions;
using Domain.Forms;
using Shared;

namespace Application.Forms.Commands.Delete;

internal sealed class DeleteFormCommandHandler : ICommandHandler<DeleteFormCommand>
{
    private readonly IFormRepository _formRepository;

    public DeleteFormCommandHandler(IFormRepository formRepository)
    {
        _formRepository = formRepository;
    }

    public async Task<Result> Handle(DeleteFormCommand request, CancellationToken cancellationToken)
    {
        var form = await _formRepository.GetFormById(request.FormId);
        if (form == null)
            return Result.Failure(FormErrors.FormNotFound());

        return await _formRepository.DeleteForm(form)
            ? Result.Success()
            : Result.Failure(Error.DBFailure);
    }
}
