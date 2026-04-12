using Application.Abstractions;
using Application.Interfaces;
using Application.Qualifications.Common;
using Domain.Qualifications;
using Shared;

namespace Application.Qualifications.Update;

internal sealed class UpdateQualificationCommandHandler : ICommandHandler<UpdateQualificationCommand>
{
    private readonly IQualificationRepository _qualificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateQualificationCommandHandler(
        IQualificationRepository qualificationRepository,
        IUnitOfWork unitOfWork)
    {
        _qualificationRepository = qualificationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateQualificationCommand request, CancellationToken cancellationToken)
    {
        var qualification = await _qualificationRepository.GetQualificationById(request.Id);

        if (qualification is null)
            return Result.Failure(Error.DBFailure);
       
        qualification.SyncUserQualifications(
            request.UserQualifications.Select(x =>
                new UserQualificationInput(
                    x.UserId,
                    x.Grade,
                    x.Description
                )
            )
        );

        return await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? Result.Success()
            : Result.Failure(Error.DBFailure);
    }
}
