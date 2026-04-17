using Application.Abstractions;
using Application.Interfaces;
using Domain.Qualifications;
using Shared;

namespace Application.Qualifications.Commands.Save;

internal sealed class SaveQualificationCommandHandler : ICommandHandler<SaveQualificationCommand>
{
    private readonly IQualificationRepository _qualificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SaveQualificationCommandHandler(
        IQualificationRepository qualificationRepository,
        IUnitOfWork unitOfWork)
    {
        _qualificationRepository = qualificationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(SaveQualificationCommand request, CancellationToken cancellationToken)
    {

        var qualification = Qualification.Create(request.TeamId, request.MinimumGrade, request.MaximumGrade, request.PassingGrade, request.Period,request.TotalGrades);
        
        foreach (var uq in request.UserQualifications)
        {
            qualification.AddUserQualification(uq.UserId, uq.Grade, uq.Description);
        }

        await _qualificationRepository.CreateQualification(qualification);

        return await _unitOfWork.SaveChangesAsync(cancellationToken)
                ? Result.Success()
                : Result.Failure(Error.DBFailure);
    }
}
