using Application.Abstractions;
using Application.Interfaces;
using Domain.Qualifications;
using Shared;

namespace Application.Qualifications.Commands.Delete;

internal sealed class DeleteQualificationCommandHandler : ICommandHandler<DeleteQualificationCommand>
{
    private readonly IQualificationRepository _qualificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteQualificationCommandHandler(
        IQualificationRepository qualificationRepository,
        IUnitOfWork unitOfWork)
    {
        _qualificationRepository = qualificationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteQualificationCommand request, CancellationToken cancellationToken)
    {
        var qualification = await _qualificationRepository.GetQualificationById(request.Id);

        if (qualification == null)
            return Result.Failure(QualificationError.QualificationNotFound(request.Id));

        _qualificationRepository.DeleteQualification(qualification);

        return await _unitOfWork.SaveChangesAsync(cancellationToken)
                ? Result.Success()
                : Result.Failure(Error.DBFailure);
    }
}
