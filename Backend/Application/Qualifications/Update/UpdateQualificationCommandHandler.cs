using Application.Abstractions;
using Application.Interfaces;
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

        if (qualification is not null)
        {
            qualification.PassingGrade = request.PassingGrade;
            qualification.MinimumGrade = request.MinimumGrade;
            qualification.Period = request.Period;
            qualification.MaximumGrade = request.MaximumGrade;
            qualification.TotalGrades = request.TotalGrades;

            foreach (var uq in request.UserQualifications)
            {
                qualification.AddUserQualification(uq.UserId, uq.Grade, uq.Position, uq.HasValue, uq.Description);
            }

            await _qualificationRepository.UpdateQualifications(qualification);

            return await _unitOfWork.SaveChangesAsync(cancellationToken)
                ? Result.Success()
                : Result.Failure(Error.DBFailure);
        }


        qualification = Qualification.Create(request.TeamId, request.MinimumGrade, request.MaximumGrade, request.PassingGrade, request.Period, request.TotalGrades);

        foreach (var uq in request.UserQualifications)
        {
            qualification.AddUserQualification(uq.UserId, uq.Grade, uq.Position, uq.HasValue, uq.Description);
        }

        await _qualificationRepository.CreateQualifications(qualification);

        return await _unitOfWork.SaveChangesAsync(cancellationToken)
                ? Result.Success()
                : Result.Failure(Error.DBFailure);
    }
}
