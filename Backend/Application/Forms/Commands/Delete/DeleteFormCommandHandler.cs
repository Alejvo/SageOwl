using Application.Abstractions;
using Application.Interfaces;
using Domain.Forms;
using Shared;

namespace Application.Forms.Commands.Delete;

internal sealed class DeleteFormCommandHandler : ICommandHandler<DeleteFormCommand>
{
    private readonly IFormRepository _formRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteFormCommandHandler(IFormRepository formRepository,IUnitOfWork unitOfWork)
    {
        _formRepository = formRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteFormCommand request, CancellationToken cancellationToken)
    {
        var form = await _formRepository.GetFormById(request.FormId);
        if (form == null)
            return Result.Failure(FormErrors.FormNotFound());

        await _formRepository.DeleteForm(form);

        return await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? Result.Success()
            : Result.Failure(Error.DBFailure);
    }
}
